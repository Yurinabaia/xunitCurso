using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._util;
using Moq;
using System;
using Xunit;
namespace CursoOnline.DominioTest.Cursos
{
  public class ArmazenadorDeCursoTest
  {
    private readonly CursoDto _cursoDto;
    private readonly ArmazenadorDeCurso _armazenadorDeCurso;
    private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

    public ArmazenadorDeCursoTest() 
    {
      var fake = new Faker();
      //Primeiro criar o DTO 
      _cursoDto = new CursoDto
      {
        Nome = fake.Random.Words(),
        Descricao = fake.Lorem.Paragraph(),
        CargaHoraria = fake.Random.Double(50, 100),
        PublicoAlvo = "Estudante",
        Valor = fake.Random.Double(50, 100)
      };

      //Criando Mock, através da Interface
       _cursoRepositorioMock = new Mock<ICursoRepositorio>();

      _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
    }

    [Fact (DisplayName = "Criacao de curso")]
    public void DeveAdicionarCurso() 
    {
      _armazenadorDeCurso.Armazenar(_cursoDto);

      //Validando se o comportamento foi validado ou não
      //Se o meu mock estive chamando o adicionar verifica se está ok
      _cursoRepositorioMock.Verify(r => 
      r.Adicionar(It.Is<Curso>(
        //Melhorando mock, validando se estes campos foram instanciado
        c => c.Nome == _cursoDto.Nome
        &&
        c.Descricao  == _cursoDto.Descricao
        )),Times.AtLeast(2) //==> Siginificar que o metodo adicionar deve ser chamado 2 vezes
        );

    }
    //Validações
    [Fact]
    public void NaoDeveInformaPublicoAlvo() 
    {
      _cursoDto.PublicoAlvo = "Medico";
      Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
        .ComMensagem("Publico Alvo inválido");


    }
  }

  public interface ICursoRepositorio
  {
    void Adicionar(Curso curso);
  }

  public class ArmazenadorDeCurso 
  {
    private readonly ICursoRepositorio _cursoRepositorio;
    public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio) 
    {
      _cursoRepositorio = cursoRepositorio;
    }
    public void Armazenar(CursoDto cursoDto) 
    {

                    //Passando o tipo     a opção a ser passada         criando a variavel   
      Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

      if (publicoAlvo == null)
        throw new ArgumentException("Publico Alvo inválido");

      //Criando objeto do curso
      var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, 
        (PublicoAlvo) publicoAlvo, cursoDto.Valor);

      //Serviço de dominio
      _cursoRepositorio.Adicionar(curso);
      _cursoRepositorio.Adicionar(curso);

    }
  }
  public class CursoDto
  {
    public string Nome { get; internal set; }
    public string Descricao { get; internal set; }
    public double CargaHoraria { get; internal set; }
    public string  PublicoAlvo { get; internal set; }
    public double Valor { get; internal set; }
  }
}
