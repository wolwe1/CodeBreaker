using System.Collections.Generic;
using System.Linq;

namespace AutomaticallyDefinedFunctions.structure.adf.helpers
{
    public class AdfOutput<T>
    {
        private List<Output<T>> _outputs;

        public AdfOutput(List<Output<T>> outputs)
        {
            _outputs = outputs;
        }

        public AdfOutput(IEnumerable<Output<T>> outputs)
        {
            _outputs = outputs.ToList();
        }

        public Output<T> GetOutput(int index)
        {
            return _outputs[index];
        }

        public List<Output<T>> GetOutputs()
        {
            return _outputs;
        }

        public IEnumerable<T> GetOutputValues()
        {
            return _outputs.Select(o => o.Value).ToList();
        }

        public void FailOutputs()
        {
            foreach (var output in _outputs)
            {
                output.Fail();
            }
        }

        public bool Failed()
        {
            return _outputs.Any(o => o.Failed);
        }
    }
}