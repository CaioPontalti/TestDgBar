# TestDgBar
Tecnologias utilizadas:
Asp.Net Core 2.2
Dapper
Json Web Token (JWT)
Swagger
Sql Server Express

*Estratégia de autenticação:
=> Utilização do JWT para controle de acesso dos end-points
  Os únicos end-points abertos são o resgister e o login. Todos os outros estão como Authorize, sendo necessário fazer o login e enviar o token a cada requisição. Para isso, caso esteja utilizando o Postman, nos Headers será necessário incluir uma key Authorization, no value informar bearer + token. Cado vá utilizar o proprio swagger, basta clicar no Authorize e informar bearer + token
  
=> Para rodar a aplicação, antes é necessário se conectar ao Sql Server e rodar o script que está no projeto de Infra/ScriptSQL. Nesse processo será criado o Database e as tabelas que serão utilizadas na app. Após isso, alterar a connection string no appsettings.json

=> Decisões tomadas
  * Decidi utilizar as tecnologias descritas por maior familiariade e conhecimento nas mesmas.
  * Serapação da solution em projetos facilitando a oramização do mesmo.
  * Criação de um command e um handler para controlar cada request.
  * Utilização da Ioc e Repository Pattern para separação da aplicação e a camada de banco.
  * Padronização do retorno da API para o front-end. Facilita na comunicação entre back e front, pois o front sempre esperará um objeto no msm formato.
  * Utilização de objetos anonimos nas execuções das queries para evitar sql injection
  
  * Tanto o número da comanda como o produção são int.
  * Só é possível inserir um item por vez, passando o numero da comanda e o id do produto (que viria de um dropdown por exemplo)
 
=> Pontos de evolução
  * Desenvolvimento do front-end. Não é minha especialidade front-end, por isso não desenvolvi a tela, porem estou estudando VueJS e a nive de consulta mues repositórios do vue estão abertos no githud
  * Maior desenvolvimento de test. Também é uma atividade que ainda preciso me especializar mais, porem dentro dos cenários do API desenvolvi alguns.
  * Arquitetura de Banco. Não tenho a experiência de um DBA então acredito que a modelagem poderia ser feita de uma forma melhor.
  * Deixei os comandos sql na camada de infra na implementação do repositorio, e as mesmas poderiam estar em procedures ou views.
  
  
  
  
  
