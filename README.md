# Rockstar Coding Challenge

Software Engineer Coding Challenge
BairesDev es una empresa de tecnología especializada en IT Staff Augmentation. Contamos con una amplia base de desarrolladores (ubicados en un 95% en latinoamérica)
que trabajan tercerizados para nuestros clientes. Buscamos ampliar nuestra cartera de clientes a través de una campaña de email marketing. Se pide desarrollar un programa que, dado un archivo de entrada **people.in** con información de perfiles públicos de LinkedIn, determine los 100 potenciales clientes con mayor probabilidad de comprar nuestros servicios. La salida esperada es un archivo **people.out** que contenga los ids de las personas que debemos contactar.

El archivo de entrada contiene, en cada línea, los siguientes campos separados por un pipe:

* PersonId, 
* Name, 
* LastName, 
* CurrentRole, 
* Country, 
* Industry, 
* NumberOfRecommendations,
* NumberOfConnections. 

Es posible que en algunos casos no conozcamos en valor de los campos, en tal caso aparecerán dos pipes consecutivos (||).

Ejemplo:
people.in

4567|arturo|perez|teleport engineering manager|Germany|Telecommunications|2|176

4568|carlos|lobalzo|jefe de gestión funcional|Argentina|Financial Services|2|259

4569|juan|martinez|repositor de mercado|Colombia|Supermarkets|0|5

4580|john|smith|co-founder &amp; cto|United States|Network Security|10|500

4592|alejandro|gonzalez|desarrollador de software|España|Telecommunications||300

…

people.out

4568

4567

4580

…

Tiempo ideal de resolución: 3-4 horas.

Intentar ajustarse al tiempo de resolución del ejercicio, incluso sabiendo que el algoritmo se podría mejorar. En tal caso, mencionar cómo se podría mejorar. ¿Qué otra información sobre las personas te sería útil para mejorar tu algoritmo?

Adicionalmente, por favor asegúrese de enfocarse en la parte funcional además de la parte técnica, pues ambos aspectos serán evaluados.

Entregar el código fuente, el ejecutable y el archivo people.out correspondiente al archivo people.in adjunto como ejemplo.

# Respuesta
Desarrollé una aplicación de consola para mantener las cosas simples.

## La solución contiene dos proyectos:
1. CommandLineApp: con funcionalidades tipo línea de comandos.
2. TestCommandLineApp: contiene test unitarios. Aquellos con postfijo "OK" ilustran los comandos soportados.

Para ejecutarla, navegar al directorio donde: ej: ``D:\Jawilliam\Jobs\BairesDev\BairesDev Coding Challenge\CommandLineApp\bin\Debug\netcoreapp3.1``

Abrir Powershell en este directorio (shift + click derecho, y seleccionar Open PowerShell window here) y ejecutar ``.\CommandLineApp.exe help`` para listar los comandos soportados:

Usage: CommandLineApp.exe [command]

Commands:

  answer             Lists los potenciales clientes, y salva sus ids in people.out.
  countries          Lists the distinct countries of people.in data.
  keywords-industry  Lists the distinct keywords used in the industry of people.in data.
  keywords-role      Lists the distinct keywords used in the role of people.in data.
  list               Lists people.in data.

Para conocer las opciones soportadas en un comando particular ejecutar ".\CommandLineApp.exe <comando> help",
e.j.: ".\CommandLineApp.exe answer help" muestra:

```
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
  ```

## La solución implementada está en:
* clase ``CommandLineApp.PeopleCommands``
* método ``Answer`` (esta es la implementación del comando **answer**).

A continuación se describe la metodología seguida para diseñar esta solución.

## Metodología:

1. ``.\CommandLineApp.exe keywords-role --view=Distinct``: Nos permite analizar las distintas palabras claves. Su listado en orden alfabético posibilita que las variantes usadas para un mismo caso (e.j: con errores ortográficos, abreviaturas) puedan ser fácilmente detectados al aparecer. Por ejemplo, los términos "analyst", "anaylst" aparecen uno debajo del otro.

	  * Los términos "advisor", "manager", "mgr": podrían ser señal de un rol con poder de decisión en la recomendación o contratación
	  de BairesDev.
    
	  * "chair", "chairman", "chief", "lead", "leader", "leading", "principal": normalmente se refiere a profesionales que toman decisiones como las tecnologías a usar, pero eventualmente pueden recomendar contrataciones de personal outsource para complementar la carga de desarrollo de sus empresas.
    
	  * "creator", "directeur", "director", "founder", "head", "owner", "president" "présidente", "proprietor", ["vice", "vp"]: generalmente identifican personas con máximo poder de decisión en sus empresas.
    
	  * "entrepreneurship" (los emprendedores normalmente requieren servicios de desarrollo de sistemas de software que soporten el despegue de sus proyectos)
    
	  * "latam", "latin" (puede denotar consorcios internacionales a los que se les pudiera vender los beneficios de BairesDev como empresa con talento distribuido en latinoamérica, e.j: zona horaria, idioma)
    
	  * "officer"

    * **FUTURE WORK**
    
	    * Seria interesante verificar qué tan informativos son los terminos: "computational", "computer", "computing", "informatique", "development", "innovation", "ecom", "e-com", "executive", "intelligence", "technology", "technologies", "telesales"
    
	    * Pudieran ser informativos dependiendo del contexto del perfil: "contractor", "coordinator", 	  , "modernization", "responsable"
	  
2. ``.\CommandLineApp.exe keywords-industry --view=Distinct``: Nos permite seleccionar distintas palabras claves
	que intuitivamente pudieran ser señal de una industria que a menudo contrata servicios de desarrollo de software.
  
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

3. ``.\CommandLineApp.exe countries --view=Distinct``
  
	  * Latinoamerica: Argentina, Brazil, Chile, Colombia, Costa Rica, Dominica, Ecuador, Mexico, Panama, Peru, Puerto Rico, Spain, Uruguay, Venezuela

4. Otras fuentes de información:
  
	  * BairesDev locations (.pdf Working at BairesDev): Argentina, Brazil, Colombia, Mexico, United States, Canada
    
	  * La experiencia indica (BairesDev Web site - Testimonials - https://www.bairesdev.com/clients/testimonials/): Product+Manager, VP+Engineering, Managing+Director, CTO, Business+Development, Director+Architecture, VP+Tecnology+Services

5. El número de conexiones puede ser un indicador (aunque no lo tengo muy claro) de posibles clientes 
  
	  * (> 0) con capacidad para comentar nuestra propuesta con su red de contactos
    
	  * (= 0) recientemente incorporándose a linkedin o que no la usa habitualmente, lo cual no significa que no sea receptivo a un correo.

    * Cargando people.in en R: summary(people[which(people$X8 > 0),]$X8)
    
    ```
		  NumberOfConnections > 0 (summary): Min.  1st Qu.  Median  Mean   3rd Qu.  Max. 
                                           1.00   3.00    21.00   95.45  128.50   500.00 
    ```

## Sería bueno:

- Identificar los SECTORES o empresas que tienden (diseñar una ontología?).

	* a contrar servicios de desarrollo de software
  
	* no contratar servicios externos porque tienen equipos propios de desarrollo de software.
  
	* tienen equipos de desarrollo propios, pero tienden a contratar personal externo eventualmente para que los apoyen eventualmente, o personal especializado para determinadas tareas.
  
- incluir información acerca de las donde trabajan estas personas. 

- Link a las empresas donde trabajan permitiría recopilar y explotar la información disponible online (scrapping). 

- Describir también los perfiles de sus conexiones.
