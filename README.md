Assignment instructions

20 minutes to complete

47 student solutions

**Requirement:**

Create
 an Asp.Net Core Web Application that demonstrates the usage of
configuration in Asp.Net Core. It displays social media links based on
the configuration settings stored in the config files.

You will store the following configuration in  **appsettings.json** :

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="pln"></span><span class="str">"SocialMediaLinks"</span><span class="pun">:</span></p></li><li class="L1" data-node-id="20231016195630-smw7yb3"><p><span class="pln"></span><span class="pun">{</span></p></li><li class="L2"><p><span class="pln"></span><span class="str">"Facebook"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.facebook.com/microsoft"</span><span class="pun">,</span></p></li><li class="L3" data-node-id="20231016195630-uxhlcyw"><p><span class="pln"></span><span class="str">"Instagram"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.instagram.com/microsoft"</span><span class="pun">,</span></p></li><li class="L4"><p><span class="pln"></span><span class="str">"Twitter"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.twitter.com/microsoft"</span><span class="pun">,</span></p></li><li class="L5" data-node-id="20231016195630-2kwb1pn"><p><span class="pln"></span><span class="str">"Youtube"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.youtube.com/microsoft"</span></p></li><li class="L6"><p><span class="pln"></span><span class="pun">}</span></p></li></ol></pre>

You
 have different links for "Development" environment. So you will store
the following social media links in appsettings.Development.json:

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="pln"></span><span class="str">"SocialMediaLinks"</span><span class="pun">:</span></p></li><li class="L1" data-node-id="20231016195630-21a9bxj"><p><span class="pln"></span><span class="pun">{</span></p></li><li class="L2"><p><span class="pln"></span><span class="str">"Facebook"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.facebook.com/dotnet"</span><span class="pun">,</span></p></li><li class="L3" data-node-id="20231016195630-m1h9rj6"><p><span class="pln"></span><span class="str">"Twitter"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.twitter.com/dotnet"</span><span class="pun">,</span></p></li><li class="L4"><p><span class="pln"></span><span class="str">"Youtube"</span><span class="pun">:</span><span class="pln"></span><span class="str">"http://www.youtube.com/dotnet"</span></p></li><li class="L5" data-node-id="20231016195630-nrorulp"><p><span class="pln"></span><span class="pun">}</span></p></li></ol></pre>

Notice, there is no instagram link for Development environment.

So you'll not display instagram link in case of Development environment.

Consider a options model class called ' **SocialMediaLinksOptions** ' with following properties:

<pre class="prettyprint linenums prettyprinted" role="presentation"><ol class="linenums"><li class="L0"><p><span class="pln"></span><span class="kwd">string</span><span class="pun">?</span><span class="pln"></span><span class="typ">Facebook</span></p></li><li class="L1" data-node-id="20231016195630-1rv090e"><p><span class="pln"></span><span class="kwd">string</span><span class="pun">?</span><span class="pln"></span><span class="typ">Instagram</span></p></li><li class="L2"><p><span class="pln"></span><span class="kwd">string</span><span class="pun">?</span><span class="pln"></span><span class="typ">Twitter</span></p></li><li class="L3" data-node-id="20231016195630-p8pdw1l"><p><span class="pln"></span><span class="kwd">string</span><span class="pun">?</span><span class="pln"></span><span class="typ">Youtube</span></p></li></ol></pre>

**Example #1:**

In
 case of any other environment - other than "Development", if you
receive a HTTP GET request at path "/", it has to generate a view with
following social media links with HTTP status code 200.

Request Url: /

Request Method: GET

Response Status Code: 200

Response body (output):

View as shown below.

![](https://img-c.udemycdn.com/redactor/raw/assignment/2023-02-13_14-36-21-37a6f22f4b9c77ba9404b7dfab321b9b.png)

**Example #2:**

In
 case of "Development" environment, if you receive a HTTP GET request at
 path "/", it has to generate a view with following social media links
with HTTP status code 200.

Request Url: /

Request Method: GET

Response Status Code: 200

Response body (output):

View as shown below.

![](https://img-c.udemycdn.com/redactor/raw/assignment/2023-02-13_14-36-21-ddd8712f83fca52cc26aa6e775a1ed56.png)

**Instructions:**

* Create controller(s) with attribute routing.
* Provide the configuration as service, using Options pattern.
* Inject the IOptions in the controller and supply social media links to the view via ViewBag.
* Use CSS styles, layout views, _ViewImports, _ViewStart as per the necessity.
* You need to hide the Instagram link in case of "Development" environment, using `<environment>` tag helper.
* The social media links (in href atribute of `<a>` tag) should be as provided in the corresponding configuration (json) mentioned above at the top.
* The CSS file is provided as downlodable resource for applying essential styles. You can download and use it.

#### Questions for this assignment

Check your source code with Instructor's source code.
