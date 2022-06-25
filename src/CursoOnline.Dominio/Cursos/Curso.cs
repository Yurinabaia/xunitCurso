using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Cursos
{
  public class Curso
  {

    public string Nome { get; private set; }
    public string Descricao { get; set; }
    public double CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }


    public Curso(string nome, string _descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
    {
      if (string.IsNullOrEmpty(nome))
        throw new ArgumentException("É esperado um nome do curso");

      if (cargaHoraria < 1)
        throw new ArgumentException("É esperado carga horaria maior que 1");

      if (valor < 1)
        throw new ArgumentException("É esperado valor maior que 1");

      Nome = nome;
      Descricao = _descricao;
      CargaHoraria = cargaHoraria;
      PublicoAlvo = publicoAlvo;
      Valor = valor;
    }

  }
}
