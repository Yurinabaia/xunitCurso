using CursoOnline.DominioTest.Cursos;

namespace CursoOnline.DominioTest._Builders
{
  public class CursoBuilder
  {
    //Atributos que serão usados no objeto anonimo
    private  string _nome = "Informatica básica";
    private  string _descricao = "Uma descrição";
    private  double _cargaHoraria = 80;
    private  PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
    private  double _valor = 950;


    //O build sempre começar com metodo static, instanciado ele mesmo
    public static CursoBuilder Novo() 
    {
      return new CursoBuilder();
    }

    //------------Definindo quais atributos vai ter o objeto que eu quero.
    public CursoBuilder ComNome(string nome) 
    {
      _nome = nome;
      return this;
    }

    public CursoBuilder ComDescricao(string descricao)
    {
      _descricao = descricao;
      return this;
    }

    public CursoBuilder ComCargaHoraria(double cargaHoraria)
    {
      _cargaHoraria = cargaHoraria;
      return this;
    }

    public CursoBuilder ComValor(double valor)
    {
      _valor = valor;
      return this;
    }

    public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
    {
      _publicoAlvo = publicoAlvo;
      return this;
    }
    public Curso Build() 
    {
      return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
    }
  }
}
