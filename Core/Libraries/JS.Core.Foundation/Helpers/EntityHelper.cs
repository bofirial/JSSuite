using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Helper for Entities
    /// </summary>
    public class EntityHelper : SingletonBase<EntityHelper>
    {
        /// <summary>
        /// Gets All Database Columns in the Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="excludePrimaryKeyColumn">if set to <c>true</c> [exclude primary key column].</param>
        /// <returns></returns>
        public IEnumerable<string> GetAllColumns<TEntity>(TEntity entity, bool excludePrimaryKeyColumn = false)
        {
            IEnumerable<PropertyInfo> properties = ReflectionHelper.Current.GetAllProperties(entity);

            properties = properties.Where(p => !ReflectionHelper.Current.HasAttribute<DBIgnoreAttribute>(p));

            if (excludePrimaryKeyColumn)
            {
                properties = properties.Where(p => !ReflectionHelper.Current.HasAttribute<DBPrimaryKeyAttribute>(p));
            }

            return properties.Select(p => p.Name);
        }

        /// <summary>
        /// Populates the Entity from a Data Reader
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public TEntity PopulateFromDataReader<TEntity>(SqlDataReader dataReader, IConnectionInfo connectionInfo)
            where TEntity : new()
        {
            TEntity entity = new TEntity();

            IEnumerable<string> propertyNames = ReflectionHelper.Current.GetAllProperties(entity).Select(p => p.Name);

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string name = dataReader.GetName(i);
                object value = dataReader.GetValue(i);
                Type type = dataReader.GetFieldType(i);

                if (value == DBNull.Value)
	            {
		            continue;
	            }

                if (type == typeof(DateTime) || (type == typeof(DateTime?)))
                {
                    value = DateTimeHelper.Current.ConvertUtcToLocal(connectionInfo, (DateTime)value);
                }

                IEnumerable<string> nameParts = name.Split('.');

                string propertyName = nameParts.Last();

                IEnumerable<string> objectNames = nameParts.Take(nameParts.Count() - 1);

                object currentObj = entity;

                foreach (string namePart in objectNames)
                {
                    if (propertyNames.Contains(namePart))
                    {
                        PropertyInfo propInfo = currentObj.GetType().GetProperty(namePart);

                        if (propInfo.GetValue(currentObj) == null)
                        {
                            ReflectionHelper.Current.SetPropertyValue(entity, namePart, Activator.CreateInstance(propInfo.PropertyType));
                        }

                        currentObj = propInfo.GetValue(currentObj);
                    }
                    else
                    {
                        continue;
                    }
                }

                if (propertyNames.Contains(propertyName))
                {
                    ReflectionHelper.Current.SetPropertyValue(currentObj, propertyName, value);
                }
            }

            return entity;
        }

        /// <summary>
        /// Returns the Table Name for an Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetTableName<TEntity>(TEntity entity)
        {
            return entity.GetType().Name;
        }

        /// <summary>
        /// Returns the Name of the Primary Key Column
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetPrimaryKeyColumn<TEntity>(TEntity entity)
        {
            IEnumerable<PropertyInfo> properties = ReflectionHelper.Current.GetAllProperties(entity);

            return properties.FirstOrDefault(p => ReflectionHelper.Current.HasAttribute<DBPrimaryKeyAttribute>(p)).Name;
        }
    }
}
