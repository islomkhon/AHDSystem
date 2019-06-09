<?php

/* @Live/index.twig */
class __TwigTemplate_b178b6485503be227949e05d6e565770d4f71cf3d2081cdcbcccec900830c5fd extends Twig_Template
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
        echo "<script type=\"text/javascript\" charset=\"utf-8\">
    \$(document).ready(function () {
        var segment = broadcast.getValueFromHash('segment');
        if (!segment) {
            segment = broadcast.getValueFromUrl('segment');
        }

        \$('#visitsLive').liveWidget({
            interval: ";
        // line 9
        echo \Piwik\piwik_escape_filter($this->env, ($context["liveRefreshAfterMs"] ?? $this->getContext($context, "liveRefreshAfterMs")), "html", null, true);
        echo ",
            onUpdate: function () {
                //updates the numbers of total visits in startbox
                var ajaxRequest = new ajaxHelper();
                ajaxRequest.setFormat('html');
                ajaxRequest.addParams({
                    module: 'Live',
                    action: 'ajaxTotalVisitors',
                    segment: segment
                }, 'GET');
                ajaxRequest.setCallback(function (r) {
                    \$(\"#visitsTotal\").html(r);
                });
                ajaxRequest.send();
            },
            maxRows: 10,
            fadeInSpeed: 600,
            dataUrlParams: {
                module: 'Live',
                action: 'getLastVisitsStart',
                segment: segment
            }
        });
    });
</script>

";
        // line 35
        if ( !($context["isWidgetized"] ?? $this->getContext($context, "isWidgetized"))) {
            echo "<div piwik-content-block content-title=\"";
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_VisitorsInRealTime")), "html_attr");
            echo "\">";
        }
        // line 36
        echo "
";
        // line 37
        $this->loadTemplate("@Live/_totalVisitors.twig", "@Live/index.twig", 37)->display($context);
        // line 38
        echo "
";
        // line 39
        echo ($context["visitors"] ?? $this->getContext($context, "visitors"));
        echo "

";
        // line 41
        ob_start();
        // line 42
        echo "<div class=\"visitsLiveFooter\">
    <a title=\"";
        // line 43
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_OnClickPause", call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_VisitorsInRealTime")))), "html", null, true);
        echo "\" href=\"javascript:void(0);\" onclick=\"onClickPause();\">
        <img id=\"pauseImage\" border=\"0\" src=\"plugins/Live/images/pause.png\" />
    </a>
    <a title=\"";
        // line 46
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_OnClickStart", call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_VisitorsInRealTime")))), "html", null, true);
        echo "\" href=\"javascript:void(0);\" onclick=\"onClickPlay();\">
        <img id=\"playImage\" style=\"display: none;\" border=\"0\" src=\"plugins/Live/images/play.png\" />
    </a>
    ";
        // line 49
        if ( !($context["disableLink"] ?? $this->getContext($context, "disableLink"))) {
            // line 50
            echo "        &nbsp;
        <a class=\"rightLink\" href=\"#\" onclick=\"this.href=broadcast.buildReportingUrl('category=General_Visitors&subcategory=Live_VisitorLog')\">";
            // line 51
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Live_LinkVisitorLog")), "html", null, true);
            echo "</a>
    ";
        }
        // line 53
        echo "</div>
";
        echo trim(preg_replace('/>\s+</', '><', ob_get_clean()));
        // line 55
        echo "
";
        // line 56
        if ( !($context["isWidgetized"] ?? $this->getContext($context, "isWidgetized"))) {
            echo "</div>";
        }
    }

    public function getTemplateName()
    {
        return "@Live/index.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  111 => 56,  108 => 55,  104 => 53,  99 => 51,  96 => 50,  94 => 49,  88 => 46,  82 => 43,  79 => 42,  77 => 41,  72 => 39,  69 => 38,  67 => 37,  64 => 36,  58 => 35,  29 => 9,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<script type=\"text/javascript\" charset=\"utf-8\">
    \$(document).ready(function () {
        var segment = broadcast.getValueFromHash('segment');
        if (!segment) {
            segment = broadcast.getValueFromUrl('segment');
        }

        \$('#visitsLive').liveWidget({
            interval: {{ liveRefreshAfterMs }},
            onUpdate: function () {
                //updates the numbers of total visits in startbox
                var ajaxRequest = new ajaxHelper();
                ajaxRequest.setFormat('html');
                ajaxRequest.addParams({
                    module: 'Live',
                    action: 'ajaxTotalVisitors',
                    segment: segment
                }, 'GET');
                ajaxRequest.setCallback(function (r) {
                    \$(\"#visitsTotal\").html(r);
                });
                ajaxRequest.send();
            },
            maxRows: 10,
            fadeInSpeed: 600,
            dataUrlParams: {
                module: 'Live',
                action: 'getLastVisitsStart',
                segment: segment
            }
        });
    });
</script>

{% if not isWidgetized %}<div piwik-content-block content-title=\"{{ 'Live_VisitorsInRealTime'|translate|e('html_attr') }}\">{% endif %}

{% include \"@Live/_totalVisitors.twig\" %}

{{ visitors|raw }}

{% spaceless %}
<div class=\"visitsLiveFooter\">
    <a title=\"{{ 'Live_OnClickPause'|translate('Live_VisitorsInRealTime'|translate) }}\" href=\"javascript:void(0);\" onclick=\"onClickPause();\">
        <img id=\"pauseImage\" border=\"0\" src=\"plugins/Live/images/pause.png\" />
    </a>
    <a title=\"{{ 'Live_OnClickStart'|translate('Live_VisitorsInRealTime'|translate) }}\" href=\"javascript:void(0);\" onclick=\"onClickPlay();\">
        <img id=\"playImage\" style=\"display: none;\" border=\"0\" src=\"plugins/Live/images/play.png\" />
    </a>
    {% if not disableLink %}
        &nbsp;
        <a class=\"rightLink\" href=\"#\" onclick=\"this.href=broadcast.buildReportingUrl('category=General_Visitors&subcategory=Live_VisitorLog')\">{{ 'Live_LinkVisitorLog'|translate }}</a>
    {% endif %}
</div>
{% endspaceless %}

{% if not isWidgetized %}</div>{% endif %}", "@Live/index.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Live\\templates\\index.twig");
    }
}
