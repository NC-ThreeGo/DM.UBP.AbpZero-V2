using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DM.Common.Extensions
{
    /// <summary>
    /// 提供简单的 object to object 的映射功能
    /// </summary>
    public static class LowBMapper
    {
        private static readonly SortedDictionary<ulong, Delegate> MappersCollection = new SortedDictionary<ulong, Delegate>();
        private static readonly SortedDictionary<ulong, Delegate> AssignersCollection = new SortedDictionary<ulong, Delegate>();


        public static MapContext<TI> Map<TI>(this TI src)
        {
            return new MapContext<TI>(src);
        }


        public class MapContext<TI>
        {
            private readonly TI _src;

            private delegate void Assign<in T, TO>(T src, ref TO target);

            public MapContext(TI src)
            {
                _src = src;
            }

            public TO To<TO>() where TO : new()
            {
                var srcType = typeof(TI);
                var targetType = typeof(TO);
                var key = ComputeMapperKey(srcType, targetType, 0x10);

                if (MappersCollection.TryGetValue(key, out var del) == false)
                {
                    var param = Expression.Parameter(srcType, "src");
                    var srcMembers = param.Type.GetFieldsAndProperties();
                    var targetMembers = targetType.GetFieldsAndProperties();
                    var memberAssignments = targetMembers.Join(srcMembers, mem => mem.Name, mem => mem.Name,
                        (targetMember, sourceMember) =>
                            Expression.Bind(targetMember, Expression.PropertyOrField(param, sourceMember.Name)));

                    var init = Expression.MemberInit(Expression.New(targetType), memberAssignments);
                    del = Expression.Lambda<Func<TI, TO>>(init, param).Compile();

                    MappersCollection.Add(key, del);
                }
                var temp = new TO();
                return ((Func<TI, TO>)del)(_src);
            }

            public void To<TO>(ref TO targetObj) where TO : new()
            {
                var srcType = typeof(TI);
                var targetType = typeof(TO);
                var key = ComputeMapperKey(srcType, targetType, 0x01);
                if (AssignersCollection.TryGetValue(key, out var del) == false)
                {
                    var param = Expression.Parameter(srcType, "src");
                    var target = Expression.Parameter(targetType.MakeByRefType(), "target");
                    var srcMembers = param.Type.GetFieldsAndProperties();
                    var targetMembers = target.Type.GetFieldsAndProperties();
                    var memberAssignments = targetMembers.Join(srcMembers, mem => mem.Name, mem => mem.Name,
                        (targetMember, sourceMember) =>
                            Expression.Assign(Expression.PropertyOrField(target, targetMember.Name), Expression.PropertyOrField(param, sourceMember.Name)));

                    var body = Expression.Block(memberAssignments);
                    del = Expression.Lambda<Assign<TI, TO>>(body, param, target).Compile();

                    MappersCollection.Add(key, del);
                }
                ((Assign<TI, TO>)del)(_src, ref targetObj);
            }
        }

        static ulong ComputeMapperKey(Type src, Type target, byte method)
        {
            return ((ulong)src.GetHashCode() << 32) | (uint)target.GetHashCode() << 8 | method;
        }
    }
}
