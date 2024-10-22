namespace API.Models;

public class Folha
    {
        public int FolhaId { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal ImpostoIrrf { get; set; }
        public decimal ImpostoInss { get; set; }
        public decimal ImpostoFgts { get; set; } // Corrigido para ImpostoFgts
        public int Quantidade { get; set; }
        public int Mes { get; set; }
        public decimal Ano { get; set; }
        public decimal SalarioLiquido { get; set; }

        // Propriedade para associar o funcionário
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; } // Navegação para o funcionário
    }