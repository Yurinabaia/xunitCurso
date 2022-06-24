using ExpectedObjects;
using System;
using System.Collections.Generic;
using Xunit;
namespace CursoOnline.DominioTest.Cursos
{
  public class CursoTest
  {
    [Fact(DisplayName = "Teste")]
    public void DeveCriarCurso()
    {

      //Organização do código

      var cursoEsperado = new //Criando objeto anonimo
      {
        Nome = "Informatica básica",
        CargaHoraria = (double) 80,
        PublicoAlvo = PublicoAlvo.Estudante,
        Valor = (double) 950
      };

      //Ação do codigo
      var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor); ;

      //Assert 

      //Assert.Equal(cursoEsperado.Nome, curso.Nome);
      //Assert.Equal(cursoEsperado.CargaHoraria, curso.CargaHoraria);
      //Assert.Equal(cursoEsperado.PublicoAlvo, curso.PublicoAlvo);
      //Assert.Equal(cursoEsperado.Valor, curso.Valor);

      cursoEsperado.ToExpectedObject().ShouldMatch(curso);//Usei a biblioteca ExpectedObject
    }



    //--------------------------Validações Baby Step----------------------------------------
    //Fiz três teste, testando cada parametro isoladamente, logo o parametro testado deve está errado e outros corretos

    //Theory cria exemplos de parametros que podem ser passados no teste
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    //Este método vai ser executado duas vezes, pois tem dois InlineData
    public void NaoDeveCursoTerNomeInvalido(string nomeInvalido) 
    {
      //Organização do código
      var cursoEsperado = new //Criando objeto anonimo
      {
        Nome = "Informatica básica",
        CargaHoraria = (double)80,
        PublicoAlvo = PublicoAlvo.Estudante,
        Valor = (double)950
      };
      //Ação
      var mensagem = Assert.Throws<ArgumentException>(() => 
            new Curso(nomeInvalido, cursoEsperado.CargaHoraria, 
            cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
      //Assert
      Assert.Equal("É esperado um nome do curso", mensagem);
    }

    //Theory cria exemplos de parametros que podem ser passados no teste
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    //Este método vai ser executado duas vezes, pois tem dois InlineData
    public void NaoDeveTerCargaHorariaMaiorQue1(double cargaHorariaInvalida)
    {
      //Organização do código
      var cursoEsperado = new //Criando objeto anonimo
      {
        Nome = "Informatica básica",
        CargaHoraria = (double)80,
        PublicoAlvo = PublicoAlvo.Estudante,
        Valor = (double)950
      };
      //Ação
      var mensagem = Assert.Throws<ArgumentException>(() =>
            new Curso(cursoEsperado.Nome, cargaHorariaInvalida,
            cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
      //Assert
      Assert.Equal("É esperado carga horaria maior que 1", mensagem);
    }


    //Theory cria exemplos de parametros que podem ser passados no teste
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    //Este método vai ser executado duas vezes, pois tem dois InlineData
    public void NaoDeveTerCursoMenorQue1(double valorInvalido)
    {
      //Organização do código
      var cursoEsperado = new //Criando objeto anonimo
      {
        Nome = "Informatica básica",
        CargaHoraria = (double)80,
        PublicoAlvo = PublicoAlvo.Estudante,
        Valor = (double)950
      };
      //Ação
      var mensagem= Assert.Throws<ArgumentException>(() =>
            new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
            cursoEsperado.PublicoAlvo, valorInvalido)).Message;
      //Assert
      Assert.Equal("É esperado valor maior que 1", mensagem);
    }
  }


  public class Curso
  {
    public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor) 
    {
      if (string.IsNullOrEmpty(nome)) 
        throw new ArgumentException("É esperado um nome do curso");

      if (cargaHoraria < 1)
        throw new ArgumentException("É esperado carga horaria maior que 1");

      if (valor < 1)
        throw new ArgumentException("É esperado valor maior que 1");

      Nome = nome;
      CargaHoraria = cargaHoraria;
      PublicoAlvo = publicoAlvo;
      Valor = valor;
    }

    public string Nome { get; private set; }
    public double  CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }
  }

  public enum PublicoAlvo 
  {
    Estudante, 
    Universitário,
    Empregado, 
    Empreendedor
  }
}
