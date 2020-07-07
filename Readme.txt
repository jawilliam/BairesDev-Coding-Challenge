Desarroll� una aplicaci�n de consola para mantener las cosas simples.

La soluci�n contiene dos proyectos:
1) CommandLineApp: con funcionalidades tipo l�nea de comandos.
2) TestCommandLineApp: contiene test unitarios. Aquellos con postfijo
"OK" ilustran los comandos soportados.

Para ejecutarla, navegar al directorio donde: ej: 
"D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1"

Abrir Powershell en este directorio (shift + click derecho, y seleccionar Open PowerShell window here) y ejecutar
".\CommandLineApp.exe help" para listar los comandos soportados:

Usage: CommandLineApp.exe [command]

Commands:

  answer             Lists los potenciales clientes, y salva sus ids in people.out.
  countries          Lists the distinct countries of people.in data.
  keywords-industry  Lists the distinct keywords used in the industry of people.in data.
  keywords-role      Lists the distinct keywords used in the role of people.in data.
  list               Lists people.in data.

Para conocer las opciones soportadas en un comando particular ejecutar ".\CommandLineApp.exe <comando> help",
e.j.: ".\CommandLineApp.exe answer help" muestra:

Lists los potenciales clientes, y salva sus ids in people.out.

Usage: CommandLineApp.exe answer [options]

Options:

  --out                     <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\people.out]
  Full path of the people.out file.

  --in                      <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\people.in]
  Full path of the people.in file.

  --keywords-role           <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\RelevantKeywords.Role.txt]
  Full path of the RelevantKeywords.Role.txt file.

  --keywords-industry       <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\RelevantKeywords.Industry.txt]
  Full path of the RelevantKeywords.Role.txt file.

  --latino-countries        <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt]
  Full path of the Prioritize.TheseCountriesBecauseOfOurExpansionInLatinoamerica.txt file.

  --ourlocations-countries  <TEXT>  [D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1\..\..\..\..\CommandLineApp\DataLake\Prioritize.TheseCountries.BecauseOfOurLocations.txt]
  Full path of the Prioritize.TheseCountries.BecauseOfOurLocations.txt file.

La soluci�n implementada est� en:
* clase "CommandLineApp.PeopleCommands"
* m�todo Answer (esta es la implementaci�n del comando "answer").

A continuaci�n se describe la metodolog�a seguida para dise�ar esta soluci�n.

Metodolog�a:
	1) ".\CommandLineApp.exe keywords-role --view=Distinct": Nos permite analizar las distintas palabras claves. 
    Su listado en orden alfab�tico posibilita que las variantes usadas para un mismo caso (e.j: con errores 
    ortogr�ficos, abreviaturas) puedan ser f�cilmente detectados al aparecer. Por ejemplo, los
    t�rminos "analyst", "anaylst" aparecen uno debajo del otro.
	  * Los t�rminos "advisor", "manager", "mgr": podr�an ser se�al de un rol con poder de decisi�n en la recomendaci�n o contrataci�n
	  de BairesDev.
	  * "chair", "chairman", "chief", "lead", "leader", "leading", "principal": normalmente se refiere a 
	  profesionales que toman decisiones como las tecnolog�as a usar, pero eventualmente pueden recomendar 
	  contrataciones de personal outsource para complementar la carga de desarrollo de sus empresas.
	  * "creator", "directeur", "director", "founder", "head", "owner", "president" "pr�sidente", "proprietor", ["vice", 
	  "vp"]: generalmente identifican personas con m�ximo poder de decisi�n en sus empresas.
	  * "entrepreneurship" (los emprendedores normalmente requieren servicios de desarrollo de sistemas de software que
	  soporten el despegue de sus proyectos)
	  * "latam", "latin" (puede denotar consorcios internacionales a los que se les pudiera vender los beneficios de
	  BairesDev como empresa con talento distribuido en latinoam�rica, e.j: zona horaria, idioma)
	  * "officer"

	  FUTURE WORK
	  * Seria interesante verificar qu� tan informativos son los terminos: "computational", "computer", "computing",
	  "informatique", "development", "innovation", "ecom", "e-com", "executive", "intelligence", "technology",
	  "technologies", "telesales"
	  * Pudieran ser informativos dependiendo del contexto del perfil: "contractor", "coordinator", 
	  , "modernization", "responsable"
	  
	2) ".\CommandLineApp.exe keywords-industry --view=Distinct": Nos permite seleccionar distintas palabras claves
	que intuitivamente pudieran ser se�al de una industria que a menudo contrata servicios de desarrollo de software.
		* Automobiles, Automation, Automotive, Car, Motor, Vehicle
		* Airlines, Aviation
		* Banking
		* Beverages
		* Biotechnology
		* Business
		* Care, Health, Medical?
		* Cosmetics
		* Delivery
		* Development
		* Electronic, Electronics
		* Entertainment, Games
		* Facilities
		* Financial, Goods, Insurance?
		* Furniture
		* Information
		* Rental, Tourism, Transportation, Travel

	3) ".\CommandLineApp.exe countries --view=Distinct"
		* Latinoamerica: Argentina, Brazil, Chile, Colombia, Costa Rica, Dominica, Ecuador, Mexico,
		Panama, Peru, Puerto Rico, Spain, Uruguay, Venezuela

	4) Otras fuentes de informaci�n:
		* BairesDev locations (.pdf Working at BairesDev): Argentina, Brazil, Colombia, Mexico, United States, Canada
		* La experiencia indica (BairesDev Web site - Testimonials - https://www.bairesdev.com/clients/testimonials/):
		Product+Manager, VP+Engineering, Managing+Director, CTO, Business+Development, Director+Architecture,
		VP+Tecnology+Services

	5) El n�mero de conexiones puede ser un indicador (aunque no lo tengo muy claro) de posibles clientes 
		* (> 0) con capacidad para comentar nuestra propuesta con su red de contactos
		* (= 0) recientemente incorpor�ndose a linkedin o que no la usa habitualmente, lo cual no significa que
		no sea receptivo a un correo.

		Cargando people.in en R: summary(people[which(people$X8 > 0),]$X8)
		NumberOfConnections > 0 (summary): Min.  1st Qu.  Median  Mean   3rd Qu.  Max. 
                                           1.00   3.00    21.00   95.45  128.50   500.00 

Ser�a bueno:
	- Identificar los SECTORES o empresas que tienden (dise�ar una ontolog�a?).
		* a contrar servicios de desarrollo de software
		* no contratar servicios externos porque tienen equipos propios de desarrollo de software.
		* tienen equipos de desarrollo propios, pero tienden a contratar personal externo eventualmente para que los 
		apoyen eventualmente, o personal especializado para determinadas tareas.
	- incluir informaci�n acerca de las donde trabajan estas personas. 
	- Link a las empresas donde trabajan permitir�a recopilar y explotar la informaci�n disponible online (scrapping). 
	- Describir tambi�n los perfiles de sus conexiones.
