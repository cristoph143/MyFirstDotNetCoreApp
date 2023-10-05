
Assignment instructions

20 minutes to complete

47 student solutions

In this assignment exercise, you will redevelop the previous assignment (Weather App) with layout views.

**Requirement: **

Imagine
 a weather application that shows weather details of the selected city.
Create an Asp.Net Core Web Application that fulfils this requirement.

Consider the following hard-coded weather data of 3 cities.

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="typ">CityUniqueCode</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"LDN"</span><span class="pun">,</span><span class="pln"></span><span class="typ">CityName</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"London"</span><span class="pun">,</span><span class="pln"></span><span class="typ">DateAndTime</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"2030-01-01 8:00"</span><span class="pun">,</span><span class="pln"></span><span class="typ">TemperatureFahrenheit</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="lit">33</span></p></li><li class="L1" data-node-id="20231005144442-otvopjb"><p><span class="typ">CityUniqueCode</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"NYC"</span><span class="pun">,</span><span class="pln"></span><span class="typ">CityName</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"London"</span><span class="pun">,</span><span class="pln"></span><span class="typ">DateAndTime</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"2030-01-01 3:00"</span><span class="pun">,</span><span class="pln"></span><span class="typ">TemperatureFahrenheit</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="lit">60</span></p></li><li class="L2"><p><span class="typ">CityUniqueCode</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"PAR"</span><span class="pun">,</span><span class="pln"></span><span class="typ">CityName</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"Paris"</span><span class="pun">,</span><span class="pln"></span><span class="typ">DateAndTime</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="str">"2030-01-01 9:00"</span><span class="pun">,</span><span class="pln"></span><span class="typ">TemperatureFahrenheit</span><span class="pln"></span><span class="pun">=</span><span class="pln"></span><span class="lit">82</span></p></li></ol></pre>

Consider a model class called ' **CityWeather** ' with following properties:

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="pln"></span><span class="kwd">string</span><span class="pln"></span><span class="typ">CityUniqueCode</span></p></li><li class="L1" data-node-id="20231005144442-owcvn2e"><p><span class="pln"></span><span class="kwd">string</span><span class="pln"></span><span class="typ">CityName</span></p></li><li class="L2"><p><span class="pln"></span><span class="typ">DateTime</span><span class="pln"></span><span class="typ">DateAndTime</span></p></li><li class="L3" data-node-id="20231005144442-eanr4y6"><p><span class="pln"></span><span class="kwd">int</span><span class="pln"></span><span class="typ">TemperatureFahrenheit</span></p></li></ol></pre>

**Example #1:**

If
 you receive a HTTP GET request at path "/", it has to generate a view
with weather details of all cities with HTTP status code 200.

Request Url: /

Request Method: GET

Response Status Code: 200

Response body (output):

View as shown below.

![](https://img-c.udemycdn.com/redactor/raw/assignment/2023-02-13_15-11-03-67006681172225de0bb688137431dc9b.png)

**Example #2:**

If
 you receive a HTTP GET request at path "/weather/{cityCode}", it has to
 generate a view with weather details of the selected city s with HTTP
status code 200.

Request Url: /

Request Method: GET

Response Status Code: 200

Response body (output):

View as shown below.

![](https://img-c.udemycdn.com/redactor/raw/assignment/2023-02-13_15-11-03-cfa1f2d74bc7a7502f4d5452041f8ad0.png)

**Instructions:**

* Create controller(s) with attribute routing.
* Initialize the hard-coded data as collection of model objects in the controller.
* Use strongly-typed views. Send model object(s) to view.
* If
  you supply an invalid city code as route parameter, it should show a
  page with proper error message, instead of throwing an exception.
* Use CSS styles, layout views, _ViewImports, _ViewStart, Partial views, Razor view engine as per the necessity.
* The
  UI should be consistent and modern. It should minimum look like as
  shown in the screenshot. Optionally, you can try enhancing it based on
  your thoughts.
* You can create a partial view that
  displays weather details of a single city; and invoke the same in
  "Index" view in foreach loop while reading city details.
* Apply
  background color for each box, based on the following categories of
  temperature value. Write local function in view, to determine the
  apppriate css class to apply background color.

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="pln"></span><span class="typ">Fahrenheit</span><span class="pln"></span><span class="kwd">is</span><span class="pln"> less than </span><span class="lit">44</span><span class="pln"></span><span class="pun">=</span><span class="pln"> blue background color</span></p></li><li class="L1" data-node-id="20231005144442-j0idwsp"><p><span class="pln"></span><span class="typ">Fahrenheit</span><span class="pln"></span><span class="kwd">is</span><span class="pln"> between </span><span class="lit">44</span><span class="pln"></span><span class="kwd">and</span><span class="pln"></span><span class="lit">74</span><span class="pln"></span><span class="pun">=</span><span class="pln"> blue background color</span></p></li><li class="L2"><p><span class="pln"></span><span class="typ">Fahrenheit</span><span class="pln"></span><span class="kwd">is</span><span class="pln"> greater than </span><span class="lit">74</span><span class="pln"></span><span class="pun">=</span><span class="pln"> blue background color</span></p></li></ol></pre>

* The CSS file is provided as downlodable resource for applying essential styles. You can download and use it.

#### Questions for this assignment

Check your source code with Instructor's source code.
