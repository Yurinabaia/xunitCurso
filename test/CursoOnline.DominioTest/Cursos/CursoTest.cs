using CursoOnline.DominioTest._util;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
  public class CursoTest
  {
    //Atributos que serão usados no objeto anonimo
    private readonly ITestOutputHelper _output;
    private readonly string _nome;
    private readonly double _cargaHoraria;
    private readonly PublicoAlvo _publicoAlvo;
    private readonly double _valor;

    //Construtor para mostra uma vez objeto anonimo
    public CursoTest(ITestOutputHelper output)
    {
      _output = output;
      _nome = "Informatica básic";
      _cargaHoraria = 80;
      _publicoAlvo = PublicoAlvo.Estudante;
      _valor = 958;
    }

    [Fact(DisplayName = "Teste")]
    public void DeveCriarCurso()
    {

      //Organização do código
      var cursoEsperado = new //Criando objeto anonimo
      {
        Nome = _nome,
        CargaHoraria = _cargaHoraria,
        PublicoAlvo = _publicoAlvo,
        Valor = _valor
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

      //Ação e Assert
      Assert.Throws<ArgumentException>(() => 
            new Curso(nomeInvalido, _cargaHoraria,
            _publicoAlvo, _valor)).ComMensagem("É esperado um nome do curso");

    }

    //Theory cria exemplos de parametros que podem ser passados no teste
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    //Este método vai ser executado três vezes, pois tem três InlineData
    public void NaoDeveTerCargaHorariaMaiorQue1(double cargaHorariaInvalida)
    {
      //Ação
      var mensagem = Assert.Throws<ArgumentException>(() =>
            new Curso(_nome, cargaHorariaInvalida,
            _publicoAlvo, _valor)).Message;
      //Assert
      Assert.Equal("É esperado carga horaria maior que 1", mensagem);
    }


    //Theory cria exemplos de parametros que podem ser passados no teste
    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-100)]
    //Este método vai ser executado três vezes, pois tem três InlineData
    public void NaoDeveTerCursoMenorQue1(double valorInvalido)
    {
      //Ação
      var mensagem= Assert.Throws<ArgumentException>(() =>
            new Curso(_nome,_cargaHoraria,
            _publicoAlvo, valorInvalido)).Message;
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
