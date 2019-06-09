<?php

/* @SiteInfoWidget/index.twig */
class __TwigTemplate_f1d1a95c0a3b3e8d0aa556a440b1b3de1d25b26459514faaa097cf67a14638d5 extends Twig_Template
{
    public function __construct(Twig_Environment $env)
    {
        parent::__construct($env);

        $this->parent = false;

        $this->blocks = array(
        );
    }

    protected function doDisplay(array $context, array $blocks = array())
    {
        // line 1
        echo "<div align=\"center\">
  <p style=\"font-size:16pt\">
    <i>";
        // line 3
        echo \Piwik\piwik_escape_filter($this->env, ($context["siteName"] ?? $this->getContext($context, "siteName")), "html", null, true);
        echo "</i>
    <br/>
    <div style=\"font-size:8pt\">Site Search Enabled: <b>";
        // line 5
        echo \Piwik\piwik_escape_filter($this->env, ($context["siteIsSiteSearchEnabled"] ?? $this->getContext($context, "siteIsSiteSearchEnabled")), "html", null, true);
        echo "</b></div>
  </p>
</div>";
    }

    public function getTemplateName()
    {
        return "@SiteInfoWidget/index.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  28 => 5,  23 => 3,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<div align=\"center\">
  <p style=\"font-size:16pt\">
    <i>{{ siteName }}</i>
    <br/>
    <div style=\"font-size:8pt\">Site Search Enabled: <b>{{ siteIsSiteSearchEnabled }}</b></div>
  </p>
</div>", "@SiteInfoWidget/index.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\SiteInfoWidget\\templates\\index.twig");
    }
}
