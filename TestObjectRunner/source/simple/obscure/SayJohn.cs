using TestObjects.source.capture;

namespace TestObjects.source.simple.obscure
{
    public class SayJohn
    {
        public CoverageResults Get(string name)
        { 
            var coverage = CoverageResults.SetupCoverage<double>("SayJohn","Get",7);
            
            coverage.AddStartNode(NodeType.If);
            if (name.Length >= 1 && name[0] == 'j')
            {
                coverage.AddNode(1,NodeType.Statement);
                var resp = "Good start!";

                if (name.Length >= 2 && name[1] == 'o')
                {
                    coverage.AddNode(2,NodeType.Statement);
                    resp = "You're on the right path";

                    if (name.Length >= 3 && name[2] == 'h')
                    {
                        coverage.AddNode(3,NodeType.Statement);
                        resp = "Just one more letter!";

                        if (name.Length >= 4 && name[3] == 'n')
                        {
                            coverage.AddNode(4,NodeType.Statement);
                            resp = "You did it little machine!";

                        }
                    }
                }
                coverage.AddNode(5,NodeType.Return);
                return coverage.SetResult(resp);
            }
            
            coverage.AddEndNode(6,NodeType.Return);
            return coverage.SetResult("You failed machine");
        }
    }
}