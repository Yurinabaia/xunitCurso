using Xunit;
namespace CursoOnline.DominioTest
{
  public class MeuPrimeiroTest
  {
    [Fact(DisplayName = "VariavelComMesmoValor")]
    public void VariavelComMesmoValor() 
    {

      //Organização 
      var variavel1 = 1;
      var variavel2 = 1;
      //Ação
      variavel1 = variavel2;
      //Assert 
      Assert.Equal(variavel1, variavel2);
    }
  }
}
