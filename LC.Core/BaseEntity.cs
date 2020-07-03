using System;

namespace LC.Core
{
    /// <summary>
    /// 实体基础类
    /// </summary>
    public abstract partial class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
