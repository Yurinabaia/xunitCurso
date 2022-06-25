using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
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
      //Aqui temos exemplode de mock 
      //Mock tem objetivo de verificar
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

    [Fact]
    public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo() 
    {
      //Aqui temos exemplo de stub, quando setapiamos o mock
      //Stub para dar comportamento ==> Setapiar.
      var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();

      //Setapiar o nome e retorno o curso já salvo
      _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

      Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
              .ComMensagem("Nome do curso já consta no banco de dados");
    }
  }
}
