using System;

namespace CursoOnline.Dominio.Cursos
{

  public class ArmazenadorDeCurso
  {
    private readonly ICursoRepositorio _cursoRepositorio;
    public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
    {
      _cursoRepositorio = cursoRepositorio;
    }
    public void Armazenar(CursoDto cursoDto)
    {
      var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);
      if (cursoJaSalvo != null)
        throw new ArgumentException("Nome do curso já consta no banco de dados");


      //Passando o tipo     a opção a ser passada         criando a variavel   
      Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

      if (publicoAlvo == null)
        throw new ArgumentException("Publico Alvo inválido");

      //Criando objeto do curso
      var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria,
        (PublicoAlvo)publicoAlvo, cursoDto.Valor);

      //Serviço de dominio
      _cursoRepositorio.Adicionar(curso);
      _cursoRepositorio.Adicionar(curso);

    }
  }
}
