<?php

/* @Marketplace/uploadPluginDialog.twig */
class __TwigTemplate_880b28d0ff0b871c4a4b6ef965f497d3dab000f1fb5d2f3dc17ab160756820be extends Twig_Template
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
        echo "<div class=\"ui-confirm\" id=\"installPluginByUpload\" piwik-plugin-upload>
    <h2>";
        // line 2
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_TeaserExtendPiwikByUpload")), "html", null, true);
        echo "</h2>

    ";
        // line 4
        if (($context["isPluginUploadEnabled"] ?? $this->getContext($context, "isPluginUploadEnabled"))) {
            // line 5
            echo "        <p class=\"description\"> ";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_AllowedUploadFormats")), "html", null, true);
            echo " </p>

        <form enctype=\"multipart/form-data\" method=\"post\" id=\"uploadPluginForm\"
              action=\"";
            // line 8
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFunction('linkTo')->getCallable(), array(array("module" => "CorePluginsAdmin", "action" => "uploadPlugin", "nonce" => ($context["installNonce"] ?? $this->getContext($context, "installNonce"))))), "html", null, true);
            echo "\">
            <input type=\"file\" name=\"pluginZip\">
            <br />
            <input class=\"startUpload btn\" type=\"submit\" value=\"";
            // line 11
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_UploadZipFile")), "html", null, true);
            echo "\">
        </form>
    ";
        } else {
            // line 14
            echo "        <p class=\"description\"> ";
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Marketplace_PluginUploadDisabled"));
            echo " </p>
        <pre>[General]
enable_plugin_upload = 1</pre>
        <input role=\"yes\" type=\"button\" value=\"";
            // line 17
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Ok")), "html", null, true);
            echo "\"/>
    ";
        }
        // line 19
        echo "</div>";
    }

    public function getTemplateName()
    {
        return "@Marketplace/uploadPluginDialog.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  60 => 19,  55 => 17,  48 => 14,  42 => 11,  36 => 8,  29 => 5,  27 => 4,  22 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<div class=\"ui-confirm\" id=\"installPluginByUpload\" piwik-plugin-upload>
    <h2>{{ 'Marketplace_TeaserExtendPiwikByUpload'|translate }}</h2>

    {% if isPluginUploadEnabled %}
        <p class=\"description\"> {{ 'Marketplace_AllowedUploadFormats'|translate }} </p>

        <form enctype=\"multipart/form-data\" method=\"post\" id=\"uploadPluginForm\"
              action=\"{{ linkTo({'module':'CorePluginsAdmin', 'action':'uploadPlugin', 'nonce': installNonce}) }}\">
            <input type=\"file\" name=\"pluginZip\">
            <br />
            <input class=\"startUpload btn\" type=\"submit\" value=\"{{ 'Marketplace_UploadZipFile'|translate }}\">
        </form>
    {% else %}
        <p class=\"description\"> {{ 'Marketplace_PluginUploadDisabled'|translate|raw }} </p>
        <pre>[General]
enable_plugin_upload = 1</pre>
        <input role=\"yes\" type=\"button\" value=\"{{ 'General_Ok'|translate }}\"/>
    {% endif %}
</div>", "@Marketplace/uploadPluginDialog.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Marketplace\\templates\\uploadPluginDialog.twig");
    }
}
