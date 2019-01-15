using System;
using System.Collections.Generic;
using ItNews.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ItNews.Data.EntityFramework
{
    public class DbScopeFactory : IDbScopeFactory
    {
        private readonly Func<DbContext> contextFactory;
        private readonly Stack<DbScope> scopes;

        public DbScopeFactory(Func<DbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
            this.scopes = new Stack<DbScope>();
        }

        public IDbScope GetNewScope()
        {
            return CreateNewScope();
        }

        public IDbScope GetSharedScope()
        {
            while (scopes.Count > 0)
            {
                var scope = scopes.Peek();
                if (scope.Disposed)
                {
                    scopes.Pop(); //remove disposed scope from stack.
                }
                else
                {
                    return new DbScope(scope); //Only root scopes (which handles db context) are stored in the stack.
                }
            }
            return CreateNewScope();
        }

        protected virtual DbScope CreateNewScope()
        {
            var scope = new DbScope(contextFactory());
            scopes.Push(scope);
            return scope;
        }
    }
}
