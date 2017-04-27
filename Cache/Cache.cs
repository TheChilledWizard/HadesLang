﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Variables;

/// <summary>
/// Storage for shared runtime data
/// </summary>
public sealed class Cache
{
    private static readonly Lazy<Cache> Lazy =
    new Lazy<Cache>(() => new Cache());
    public static Cache Instance { get { return Lazy.Value; } }
    /// <summary>
    /// Var store
    /// </summary>
    public Dictionary<Tuple<string,string>, Types> Variables { get; set; }
    public bool EraseVars { get; set; } = true;
    public List<string> LoadFiles { get; set; } = new List<string>();
    private Cache()
    {
    }
}
