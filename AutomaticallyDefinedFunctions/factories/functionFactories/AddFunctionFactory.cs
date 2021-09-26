using System;
using AutomaticallyDefinedFunctions.factories.addFunction;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public class AddFunctionFactory : FunctionFactory
    {
        public AddFunctionFactory(): base(NodeCategory.Add){}
        public static AddFunc<T> CreateAddFunction<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (AddFunc<T>) (object) new StringAddFunction();
            }

            if (typeof(T) == typeof(double))
            {
                return (AddFunc<T>)  (object) new NumericAddFunc();
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (AddFunc<T>)  (object) new BooleanAddFunc();
            }

            throw new InvalidOperationException($"Unable to generate add function of type {typeof(T)}");
        }

        public override FunctionNode<T> Get<T, TU>(int maxDepth, FunctionGenerator parent)
        {
            var addFunc = AddFunctionFactory.CreateAddFunction<T>();

            var firstChild = parent.Choose<T>(maxDepth - 1);
            var secondChild = parent.Choose<T>(maxDepth - 1);

            return addFunc.AddChild(firstChild).AddChild(secondChild);
        }

        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(string) || t == typeof(double) || t == typeof(bool);
        }

        protected override INode<T> GenerateFunction<T, TU>(string id, FunctionGenerator functionGenerator)
        {
            var leftChild = functionGenerator.GenerateChildFromId<T>(ref id);
            
            var rightChild = functionGenerator.GenerateChildFromId<T>(ref id);
            
            var addFunc = AddFunctionFactory
                .CreateAddFunction<T>()
                .AddChild(leftChild)
                .AddChild(rightChild);
            
            return addFunc;
        }
    }
}