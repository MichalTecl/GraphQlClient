using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    public enum FieldInclusionMode
    {
        /// <summary>
        /// The field will be included in the query if it is of primitive type. Complex types will be included only using .With(...)
        /// </summary>
        Default,

        /// <summary>
        /// The field will be excluded unless it's included by .With(...)
        /// </summary>
        Exclude,

        /// <summary>
        /// The field will be included and automatically populated
        /// </summary>
        Include
    }
}
