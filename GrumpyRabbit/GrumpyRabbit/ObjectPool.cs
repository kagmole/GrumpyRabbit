using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit
{
    /// <summary>
    /// Utility class to create object pool.
    /// 
    /// For the moment, it is only used by the WorldGenerator.
    /// The "new" operator is one of the most exansive command in high-level
    /// programming language. The WorldGenerator will create approximately 100 objects
    /// per second, and every one of them would be collected by the garbage collector.
    /// Because it would decrease the FPS rate, we use an object pool to prevent the
    /// garbage collector to collect some referenced objects.
    /// </summary>
    /// <typeparam name="T">Type of the object for this pool</typeparam>
    public class ObjectPool<T>
    {
        /// <summary>
        /// Method used when we don't have anymore freed objects. It will then create
        /// a new object with the "new" operator.
        /// 
        /// Beware that beyond the object pool capacity, this method will always be
        /// called and will reduce the FPS rate.
        /// </summary>
        /// <param name="args">Boxing arguments for the object</param>
        /// <returns>New object of the pool</returns>
        public delegate T CreateObjectMethod(params object[] args);

        /// <summary>
        /// Method used when we still have some freed objects. It will "recycle" an
        /// old object, without the "new" operator.
        /// 
        /// This method is a lot more efficient than is accomplice, "CreateObjectMethod".
        /// </summary>
        /// <param name="recycledObject">Old object to be recycled</param>
        /// <param name="args">Boxing arguments for the object</param>
        public delegate void RecycleObjectMethod(T recycledObject, params object[] args);

        private CreateObjectMethod createObjectMethod;
        private RecycleObjectMethod recycleObjectMethod;

        /// <summary>
        /// Freed objects. It starts at 0 and stocks freed objects with the "FreeObject"
        /// method as long as it doesn't exceed the stack capacitiy (equals to the pool capacity).
        /// </summary>
        private Stack<T> freedObjects;

        private int capacity;

        /// <summary>
        /// Main constructor. Needs methods to create and recycle objects and the capacity of the pool.
        /// </summary>
        /// <param name="createObjectMethod">Method used to create an object (with "new")</param>
        /// <param name="recycleObjectMethod">Method used to recycle an object (without "new")</param>
        /// <param name="capacity">Pool capacity (how many freed objects do we stock)</param>
        public ObjectPool(CreateObjectMethod createObjectMethod, RecycleObjectMethod recycleObjectMethod, int capacity)
        {
            this.createObjectMethod = createObjectMethod;
            this.recycleObjectMethod = recycleObjectMethod;

            this.capacity = capacity;
            this.freedObjects = new Stack<T>(capacity);
        }

        /// <summary>
        /// The pool method to create objects. It will check if the stack have some freed objects to
        /// recycle, otherwise it will create a new object. Both operations use delegates methods given
        /// above.
        /// </summary>
        /// <param name="args">Boxing arguments for the object</param>
        /// <returns>Object of the pool</returns>
        public T SeekObject(params object[] args)
        {
            /* "default" returns null if T is a Ref Type or 0 if T is a Value Type. */
            T soughtObject = default(T);

            if (freedObjects.Count == 0)
            {
                soughtObject = createObjectMethod(args);
            }
            else
            {
                soughtObject = freedObjects.Pop();
                recycleObjectMethod(soughtObject, args);
            }

            return soughtObject;
        }

        /// <summary>
        /// The pool method to destroy objects. It will check if the stack is not full to stock the
        /// given object. Otherwise, the garbage collector will take care of it.
        /// </summary>
        /// <param name="freedObject">Object of the pool to "destroy"</param>
        public void FreeObject(T freedObject)
        {
            if (freedObjects.Count < capacity)
            {
                freedObjects.Push(freedObject);
            }
        }
    }
}