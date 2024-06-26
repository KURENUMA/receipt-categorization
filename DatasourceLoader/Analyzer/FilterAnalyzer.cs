﻿using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dma.DatasourceLoader.Analyzer
{
    public class FilterAnalyzer<T> : IFilterAnalyzer where T : class
    {
        public FilterOption option;
        private Stack<(string, Type, FilterCreatorTypes)> properties = new Stack<(string, Type, FilterCreatorTypes)>();

        public FilterAnalyzer(FilterOption option)
        {
            this.option = option;
            DiscoverProperties();
        }

        public List<IFilterCreator> Creators { get ; set ; } = new List<IFilterCreator>();

      

        public static bool IsCustomClass(Type type)
        {
            return type.IsClass && !type.IsPrimitive && !type.IsValueType && type != typeof(string);
        }

        private static bool IsTypeIEnumerable(Type type)
        {
            Type ienumerableGenericType = typeof(IEnumerable<>);

            // Check if the type implements IEnumerable<T>
            return !type.Equals(typeof(string)) && type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == ienumerableGenericType);
        }

        private void DiscoverProperties()
        {
            string[] parts = option.PropertyName.Split('.');
            Type parentType = typeof(T);
            foreach (string part in parts)
            {
                //var propertyInfo = parentType.GetProperty(part);
                var propertyInfo = parentType.GetProperty("View")?.PropertyType.GetProperty(part);

                if (propertyInfo == null) throw new MissingMemberException(part);
                if (IsTypeIEnumerable(propertyInfo.PropertyType))
                {
                    Type listElementType = propertyInfo.PropertyType.GetGenericArguments()[0];
                    properties.Push((part, parentType, FilterCreatorTypes.Navigation));
                    parentType = listElementType;
                    continue;
                }
                if(IsCustomClass(propertyInfo.PropertyType))
                {
                    properties.Push((part, parentType, FilterCreatorTypes.Nested));
                    parentType = propertyInfo.PropertyType;
                    continue;
                }
                properties.Push((part, parentType, FilterCreatorTypes.Primary));
                parentType = propertyInfo.PropertyType;
            }
        }

        public List<IFilterCreator> GetCreators()
        {
            List<IFilterCreator> creators = new List<IFilterCreator>();
            var properties = new Stack<(string, Type, FilterCreatorTypes)>(this.properties.Reverse());

            while (properties.Count > 0)
            {
                var (part, type, filterType) = properties.Pop();
                
                if (filterType == FilterCreatorTypes.Navigation)
                {
                    var navCreator = new NavigationFilterCreator(part, creators.Last(), type);
                    creators.Add(navCreator);
                    continue;
                }
                if (filterType == FilterCreatorTypes.Nested)
                {
                    var nestedCreator = new NestedFilterCreator(creators.Last(), part, type);
                    creators.Add(nestedCreator);
                    continue;
                }

                Type creatorType = typeof(PrimaryFilterCreator<>).MakeGenericType(type);
                var creator = (IFilterCreator)Activator.CreateInstance(creatorType, new FilterOption(part, option.Operator, option.Value));

                creators.Add(creator);
            }
         

            return creators;
        }

       
    }
}
