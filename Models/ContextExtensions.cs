using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System;

namespace SIG2Server.Models
{
    public static class ContextExtensions  
    {  
        public static void AddOrUpdate<TEntity>(this DbSet<TEntity> set, TEntity entity, long id) 
        where TEntity : class  
        {  
            try
                {
                var foundedEntity = set.Find(id);
                if (foundedEntity == null)
                {
                    set.Add(entity);
                } else 
                {
                    set.Update(entity);
                }
            } catch (Exception e)
            {
                Console.WriteLine("Source: " + e.Source);
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("HelpLink: " + e.HelpLink);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }  
    } 
}