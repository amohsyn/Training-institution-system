using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity
{
    public static class DbContextExtensions
    {
        public static string[] GetKeyNames<TEntity>(this DbContext context)
            where TEntity : class
        {
            return context.GetKeyNames(typeof(TEntity));
        }

         

        public static string[] GetKeyNames(this DbContext context, Type entityType)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the mapping between CLR types and metadata OSpace
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get metadata for given CLR type
            var entityMetadata = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == entityType);
            
            return entityMetadata.KeyProperties.Select(p => p.Name).ToArray();
        }

        public static string[] GetForeignKeyNames(this DbContext context, Type entityType)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the mapping between CLR types and metadata OSpace
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get metadata for given CLR type
            var entityMetadata = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == entityType);

           
            return entityMetadata.NavigationProperties
                .Where(p=>(p.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One || p.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne))
                .Select(p => p.Name).ToArray();
        }

        public static string[] Get_Many_ForeignKeyNames(this DbContext context, Type entityType)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the mapping between CLR types and metadata OSpace
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get metadata for given CLR type
            var entityMetadata = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == entityType);


            return entityMetadata.NavigationProperties
                .Where(p => p.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
                .Select(p => p.Name).ToArray();
        }

        /// <summary>
        /// Get ForeignKeys_Ids as GroupId
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typeEntity"></param>
        /// <returns></returns>
        public static List<string> GetForeignKeysIds(this DbContext context, Type typeEntity)
        {
            EntityType entityType = context.getEntityType(typeEntity);
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            List<string> ForeignKeys = new List<string>();

            // [Bug] the foreign key may be named diffrente of [EntityName + Id]
            for (int i = 0; i < NavigationMembers.Count(); i++)
            {
                ForeignKeys.Add(NavigationMembers[i] + "Id");
            }
            return ForeignKeys;
        }

        public static EntityType getEntityType(this DbContext context, Type entityType)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the mapping between CLR types and metadata OSpace
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get metadata for given CLR type
            var entityMetadata = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == entityType);


            return entityMetadata;
        }

        public static List<Type> GetAllTypesInContextOrder(this DbContext context)
        {
            var sets = from p in context.GetType().GetProperties() where p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) let entityType = p.PropertyType.GetGenericArguments().First() select p.PropertyType.GetGenericArguments()[0];
            return sets.ToList<Type>();
        }


      

    }
}