using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;

namespace AutomaticallyDefinedFunctions.generators
{
    public static class FunctionPicker
    {
        public static TFactory PickFactoryAs<T,TFactory>(IEnumerable<IDispatcher> factories)
        {
            var factoriesThatCanDispatch = factories
                .Where(f => f.CanDispatch<T>()).ToList();
            
            var choice = RandomNumberFactory.Next(factoriesThatCanDispatch.Count);

            return (TFactory)factoriesThatCanDispatch.ElementAt(choice);
        }
        
        public static TFactory PickFactoryWithAuxAs<T,TU,TFactory>(IEnumerable<IDispatcher> factories)
        {
            var factoriesThatCanDispatchPrimary = factories
                .Where(f => f.CanDispatch<T>());
            
            var secondaryDispatchCapable = factoriesThatCanDispatchPrimary
                .Where(f => f.CanDispatchAux<TU>()).ToList();

            var choice = RandomNumberFactory.Next(secondaryDispatchCapable.Count);

            return (TFactory)secondaryDispatchCapable.ElementAt(choice);
        }
    }
}