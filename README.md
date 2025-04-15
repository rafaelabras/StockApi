# StockApi
criação de uma API para experimentar o uso do EntityFramework e ORM, implementar em prática DI, DTOs, Metodos Assíncronos tal quanto explorar boas práticas.

Explicação da implementação de cada conceito na API:

EntityFramework e ORM:



DTos:




DI:



Async/Metodos Assíncronos: Ao criar a API e realizar estudos autonomos, compreendi a necessidade e a forma que o async funciona no .NET .Ao designar uma thread para realizar um metodo, dentro desse metodo pode haver uma operação
como consulta ao banco de dados que pode levar um tempo indeterminado para ocorrer, qual seria a necessidade de bloquear a thread enquanto a consulta é realizada? através desse contexto aparece a utilização do await, que ira liberar
a thread para o ThreadPool realizar outra ação e assim otimizar a API, pois, ao invés de perder o tempo esperando a resposta do banco de dados (nessa espera a thread que estava ocupada faz absolutamente nada), essa mesma thread poderia ser usada 
em uma nova chamada a API. Assim tornando o App, Site... escalável, ao suportar mais requisições.

Também ao aprofundar nos estudos compreendi o conceito de State Machine e do POR QUE o c# cria uma ao usuário declarar um metodo async, pelo mesmo motivo da thread ser liberada ao encontrar um await no código, como o C# "saberia"
onde o código parou ate o await, como ficaria o escopo das variáveis? o C# ao criar a State Machine, também cria dentro dela uma classe interna que representa o escopo local das variáveis do metodo assincrono, e assim sempre é possível manter o escopo local das variáveis 
ao uma thread ser liberade em um await e depois outra thread retomar sua execução. Outra parte muito interessante do funcionamento de uma State Machine são os awaits, o await basicamente "quebra" a assíncronização em pedaços, e ao realizar cada etapa e ao criar a propriedade
"State" dentro da state machine é possível entender em qual await a State Machine está, atualizando o valor de STATE para retornar a execução no ponto certo. Aqui vai um trecho de código para compreender melhor de forma simplificada:

       switch (state)
        {
            case 0:
                resultado1 = await Tarefa1(); 
                estado = 1; 
                return; 

            case 1:

                resultado2 = await Tarefa2(); 
                Console.WriteLine($"Resultado Final: {resultado1 + resultado2}");
                break;
        }

o fluxo é algo parecido com isso para saber onde o código parou, não sei se é absoluto porem percebi que quando há apenas um await no código é utilizado um IF statement ao invés do Switch Case como no caso q demonstrei.

