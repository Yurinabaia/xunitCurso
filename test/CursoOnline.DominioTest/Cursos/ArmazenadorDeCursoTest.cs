using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;
namespace CursoOnline.DominioTest.Cursos
{
  public class ArmazenadorDeCursoTest
  {
    [Fact (DisplayName = "Criacao de curso")]
    public void DeveAdicionarCurso() 
    {
      var cursoDto = new CursoDto
      {
        Nome = "CursoA",
        Descricao = "Descricao",
        CargaHoraria = 80,
        PublicoAlvoId = 1,
        Valor = 80
      };
      //Criando Mock
      var cursoRepositorioMock = new Mock<ICursoRepositorio>();

      var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

      armazenadorDeCurso.Armazenar(cursoDto);

      //Validando se o comportamento foi validado ou não
      cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
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
      //Criando objeto do curso
      var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, 
        PublicoAlvo.Estudante, cursoDto.Valor);

      //Serviço de dominio
      _cursoRepositorio.Adicionar(curso);
    } 
  }
  public class CursoDto
  {
    public string Nome { get; internal set; }
    public string Descricao { get; internal set; }
    public int CargaHoraria { get; internal set; }
    public int PublicoAlvoId { get; internal set; }
    public int Valor { get; internal set; }
  }
}
