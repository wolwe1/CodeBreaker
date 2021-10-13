using System;

namespace AutomaticallyDefinedFunctions.factories
{
    public interface IDispatcher
    {
        bool CanDispatch<T>();
        
        bool CanDispatchAux<T>();
    }
}