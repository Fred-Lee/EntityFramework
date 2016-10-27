// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Clauses.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public abstract class RecursiveQueryModelVisitorBase : QueryModelVisitorBase
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void VisitQueryModel(QueryModel queryModel)
        {
            base.VisitQueryModel(queryModel);

            queryModel.TransformExpressions(new RecursiveQueryModelExpressionVisitor(this).Visit);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        private class RecursiveQueryModelExpressionVisitor : ExpressionVisitorBase
        {
            private readonly RecursiveQueryModelVisitorBase _parentVisitor;

            public RecursiveQueryModelExpressionVisitor(RecursiveQueryModelVisitorBase parentVisitor)
            {
                _parentVisitor = parentVisitor;
            }

            protected override Expression VisitSubQuery(SubQueryExpression expression)
            {
                _parentVisitor.VisitQueryModel(expression.QueryModel);

                return base.VisitSubQuery(expression);
            }
        }
    }
}
