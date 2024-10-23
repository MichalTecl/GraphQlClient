﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel
{
    public interface INode
    {
        string NodeId { get; }
        int Level { get; }
        INode Parent { get; set; }
        NodeCollection Nodes { get; }
        bool IsImportant { get; }
        void Render(StringBuilder sb);
    }
}
