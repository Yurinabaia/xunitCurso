using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Cursos
{
  public interface ICursoRepositorio
  {
    void Adicionar(Curso curso);
    Curso ObterPeloNome(string nome);
  }
}
