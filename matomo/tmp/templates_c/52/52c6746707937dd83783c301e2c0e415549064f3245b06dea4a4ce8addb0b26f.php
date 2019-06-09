<?php

/* @Annotations/getEvolutionIcons.twig */
class __TwigTemplate_6a5239ca9fc29fd811306d6b97a92d6ba6cd024c2f6468bd91ee7e8e36c1a45b extends Twig_Template
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
        echo "<div class=\"evolution-annotations\">
    ";
        // line 2
        $context['_parent'] = $context;
        $context['_seq'] = twig_ensure_traversable(($context["annotationCounts"] ?? $this->getContext($context, "annotationCounts")));
        foreach ($context['_seq'] as $context["_key"] => $context["dateCountPair"]) {
            // line 3
            echo "        ";
            $context["date"] = $this->getAttribute($context["dateCountPair"], 0, array(), "array");
            // line 4
            echo "        ";
            $context["counts"] = $this->getAttribute($context["dateCountPair"], 1, array(), "array");
            // line 5
            echo "        <span data-date=\"";
            echo \Piwik\piwik_escape_filter($this->env, ($context["date"] ?? $this->getContext($context, "date")), "html", null, true);
            echo "\" data-count=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["counts"] ?? $this->getContext($context, "counts")), "count", array()), "html", null, true);
            echo "\" data-starred=\"";
            echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute(($context["counts"] ?? $this->getContext($context, "counts")), "starred", array()), "html", null, true);
            echo "\"
              ";
            // line 6
            if (($this->getAttribute(($context["counts"] ?? $this->getContext($context, "counts")), "count", array()) == 0)) {
                echo "title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Annotations_AddAnnotationsFor", ($context["date"] ?? $this->getContext($context, "date")))), "html", null, true);
                echo "\"
              ";
            } elseif (($this->getAttribute(            // line 7
($context["counts"] ?? $this->getContext($context, "counts")), "count", array()) == 1)) {
                echo "title=\"";
                echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Annotations_AnnotationOnDate", ($context["date"] ?? $this->getContext($context, "date")), \Piwik\piwik_escape_filter($this->env, $this->getAttribute(                // line 8
($context["counts"] ?? $this->getContext($context, "counts")), "note", array()), "html_attr")));
                echo "
";
                // line 9
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Annotations_ClickToEditOrAdd")), "html", null, true);
                echo "\"
              ";
            } else {
                // line 10
                echo "}title=\"";
                echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("Annotations_ViewAndAddAnnotations", ($context["date"] ?? $this->getContext($context, "date")))), "html", null, true);
                echo "\"";
            }
            echo ">
            <span class=\"icon-annotation ";
            // line 11
            if (($this->getAttribute(($context["counts"] ?? $this->getContext($context, "counts")), "starred", array()) > 0)) {
                echo "starred";
            }
            echo "\"/>
        </span>
    ";
        }
        $_parent = $context['_parent'];
        unset($context['_seq'], $context['_iterated'], $context['_key'], $context['dateCountPair'], $context['_parent'], $context['loop']);
        $context = array_intersect_key($context, $_parent) + $_parent;
        // line 14
        echo "</div>
";
    }

    public function getTemplateName()
    {
        return "@Annotations/getEvolutionIcons.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  77 => 14,  66 => 11,  59 => 10,  54 => 9,  50 => 8,  47 => 7,  41 => 6,  32 => 5,  29 => 4,  26 => 3,  22 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<div class=\"evolution-annotations\">
    {% for dateCountPair in annotationCounts %}
        {% set date=dateCountPair[0] %}
        {% set counts=dateCountPair[1] %}
        <span data-date=\"{{ date }}\" data-count=\"{{ counts.count }}\" data-starred=\"{{ counts.starred }}\"
              {% if counts.count == 0 %}title=\"{{ 'Annotations_AddAnnotationsFor'|translate(date) }}\"
              {% elseif counts.count == 1 %}title=\"{{ 'Annotations_AnnotationOnDate'|translate(date,
                  (counts.note|e('html_attr')))|raw }}
{{ 'Annotations_ClickToEditOrAdd'|translate }}\"
              {% else %}}title=\"{{ 'Annotations_ViewAndAddAnnotations'|translate(date) }}\"{% endif %}>
            <span class=\"icon-annotation {% if counts.starred > 0 %}starred{% endif %}\"/>
        </span>
    {% endfor %}
</div>
", "@Annotations/getEvolutionIcons.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\Annotations\\templates\\getEvolutionIcons.twig");
    }
}
