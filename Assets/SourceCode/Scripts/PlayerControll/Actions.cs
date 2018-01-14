using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevsSourceCode
{
    namespace InputHandling
    {
        namespace ActionSystem
        {
            public interface Action
            {
                void Execute(GameObject target);
            }

            public class EmptyAction : Action
            {
                public void Execute(GameObject target)
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
}

namespace TestExampleActions
{
    using GameDevsSourceCode.InputHandling.ActionSystem;
    public class JumpAction : Action
    {
        public void Execute(GameObject target)
        {
            Debug.Log("Jump!");
        }
    }
}