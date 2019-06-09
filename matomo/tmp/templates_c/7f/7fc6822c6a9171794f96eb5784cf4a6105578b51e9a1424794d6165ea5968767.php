<?php

/* @ScheduledReports/_addReport.twig */
class __TwigTemplate_65a5ae0ddf306e5043092c6883e6be921af8283bd38546cc0a770a4914f5e810 extends Twig_Template
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
        echo "<div piwik-content-block
     content-title=\"";
        // line 2
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_CreateAndScheduleReport")), "html_attr");
        echo "\"
     class=\"entityAddContainer\"
     ng-if=\"manageScheduledReport.showReportForm\">
    <div class='clear'></div>
    <form id='addEditReport' piwik-form ng-submit=\"manageScheduledReport.submitReport()\">

        <div piwik-field uicontrol=\"text\" name=\"website\"
             title=\"";
        // line 9
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Website")), "html_attr");
        echo "\"
             data-disabled=\"true\"
             value=\"";
        // line 11
        echo call_user_func_array($this->env->getFilter('rawSafeDecoded')->getCallable(), array(($context["siteName"] ?? $this->getContext($context, "siteName"))));
        echo "\">
        </div>

        <div piwik-field uicontrol=\"textarea\" name=\"report_description\"
             title=\"";
        // line 15
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_Description")), "html_attr");
        echo "\"
             ng-model=\"manageScheduledReport.report.description\"
             inline-help=\"";
        // line 17
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_DescriptionOnFirstPage")), "html_attr");
        echo "\">
        </div>

        ";
        // line 20
        if (($context["segmentEditorActivated"] ?? $this->getContext($context, "segmentEditorActivated"))) {
            // line 21
            echo "            <div id=\"reportSegmentInlineHelp\" class=\"inline-help-node\">
                ";
            // line 22
            ob_start();
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("SegmentEditor_DefaultAllVisits")), "html", null, true);
            $context["SegmentEditor_DefaultAllVisits"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 23
            echo "                ";
            ob_start();
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("SegmentEditor_AddNewSegment")), "html", null, true);
            $context["SegmentEditor_AddNewSegment"] = ('' === $tmp = ob_get_clean()) ? '' : new Twig_Markup($tmp, $this->env->getCharset());
            // line 24
            echo "                ";
            echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_Segment_Help", "<a href=\"./\" rel=\"noreferrer noopener\" target=\"_blank\">", "</a>", ($context["SegmentEditor_DefaultAllVisits"] ?? $this->getContext($context, "SegmentEditor_DefaultAllVisits")), ($context["SegmentEditor_AddNewSegment"] ?? $this->getContext($context, "SegmentEditor_AddNewSegment"))));
            echo "
            </div>

            <div piwik-field uicontrol=\"select\" name=\"report_segment\"
                 ng-model=\"manageScheduledReport.report.idsegment\"
                 options=\"";
            // line 29
            echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter(($context["savedSegmentsById"] ?? $this->getContext($context, "savedSegmentsById"))), "html", null, true);
            echo "\"
                 title=\"";
            // line 30
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("SegmentEditor_ChooseASegment")), "html_attr");
            echo "\"
                 inline-help=\"#reportSegmentInlineHelp\">
            </div>
        ";
        }
        // line 34
        echo "
        <div id=\"emailScheduleInlineHelp\" class=\"inline-help-node\">
            ";
        // line 36
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_WeeklyScheduleHelp")), "html", null, true);
        echo "
            <br/>
            ";
        // line 38
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_MonthlyScheduleHelp")), "html", null, true);
        echo "
        </div>

        <div piwik-field uicontrol=\"select\" name=\"report_period\"
             options=\"";
        // line 42
        echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter(($context["periods"] ?? $this->getContext($context, "periods"))), "html", null, true);
        echo "\"
             ng-model=\"manageScheduledReport.report.period\"
             title=\"";
        // line 44
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_EmailSchedule")), "html_attr");
        echo "\"
             inline-help=\"#emailScheduleInlineHelp\">
        </div>

        <div piwik-field uicontrol=\"select\" name=\"report_hour\"
             options=\"manageScheduledReport.reportHours\"
             ng-change=\"manageScheduledReport.updateReportHourUtc()\"
             ng-model=\"manageScheduledReport.report.hour\"
             ";
        // line 52
        if ((($context["timezoneOffset"] ?? $this->getContext($context, "timezoneOffset")) != 0)) {
            echo "inline-help=\"#reportHourHelpText\"";
        }
        // line 53
        echo "             title=\"";
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_ReportHour", "X")), "html_attr");
        echo "\">
        </div>

        ";
        // line 56
        if ((($context["timezoneOffset"] ?? $this->getContext($context, "timezoneOffset")) != 0)) {
            // line 57
            echo "            <div id=\"reportHourHelpText\" class=\"inline-help-node\">
                <span ng-bind=\"manageScheduledReport.report.hourUtc\"></span>
            </div>
        ";
        }
        // line 61
        echo "
        <div piwik-field uicontrol=\"select\" name=\"report_type\"
             options=\"";
        // line 63
        echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter(($context["reportTypeOptions"] ?? $this->getContext($context, "reportTypeOptions"))), "html", null, true);
        echo "\"
             ng-model=\"manageScheduledReport.report.type\"
             ng-change=\"manageScheduledReport.changedReportType()\"
             ";
        // line 66
        if ((twig_length_filter($this->env, ($context["reportTypes"] ?? $this->getContext($context, "reportTypes"))) == 1)) {
            echo "disabled=\"true\"";
        }
        // line 67
        echo "             title=\"";
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_ReportType")), "html_attr");
        echo "\">
        </div>

        ";
        // line 70
        $context['_parent'] = $context;
        $context['_seq'] = twig_ensure_traversable(($context["reportFormatsByReportTypeOptions"] ?? $this->getContext($context, "reportFormatsByReportTypeOptions")));
        foreach ($context['_seq'] as $context["reportType"] => $context["reportFormats"]) {
            // line 71
            echo "            <div piwik-field uicontrol=\"select\" name=\"report_format\"
                 class=\"";
            // line 72
            echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
            echo "\"
                 ng-model=\"manageScheduledReport.report.format";
            // line 73
            echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
            echo "\"
                 ng-show=\"manageScheduledReport.report.type == '";
            // line 74
            echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
            echo "'\"
                 options=\"";
            // line 75
            echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter($context["reportFormats"]), "html", null, true);
            echo "\"
                 title=\"";
            // line 76
            echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_ReportFormat")), "html_attr");
            echo "\">
            </div>
        ";
        }
        $_parent = $context['_parent'];
        unset($context['_seq'], $context['_iterated'], $context['reportType'], $context['reportFormats'], $context['_parent'], $context['loop']);
        $context = array_intersect_key($context, $_parent) + $_parent;
        // line 79
        echo "
        ";
        // line 80
        echo call_user_func_array($this->env->getFunction('postEvent')->getCallable(), array("Template.reportParametersScheduledReports"));
        echo "

        <div ng-show=\"manageScheduledReport.report.type == 'email' && manageScheduledReport.report.formatemail !== 'csv'\">
            <div piwik-field uicontrol=\"select\" name=\"display_format\" class=\"email\"
                 ng-model=\"manageScheduledReport.report.displayFormat\"
                 options=\"";
        // line 85
        echo \Piwik\piwik_escape_filter($this->env, twig_jsonencode_filter(($context["displayFormats"] ?? $this->getContext($context, "displayFormats"))), "html", null, true);
        echo "\"
                 introduction=\"";
        // line 86
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_AggregateReportsFormat")), "html_attr");
        echo "\">
            </div>

            <div piwik-field uicontrol=\"checkbox\" name=\"report_evolution_graph\"
                 class=\"report_evolution_graph\"
                 ng-model=\"manageScheduledReport.report.evolutionGraph\"
                 ng-show=\"manageScheduledReport.report.displayFormat == '2' || manageScheduledReport.report.displayFormat == '3'\"
                 title=\"";
        // line 93
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_EvolutionGraph", 5)), "html_attr");
        echo "\">
            </div>

            <div
                class=\"row evolution-graph-period\"
                ng-show=\"manageScheduledReport.report.displayFormat == '1' || manageScheduledReport.report.displayFormat == '2' || manageScheduledReport.report.displayFormat == '3'\"
            >
                <div class=\"col s12\">
                    <input id=\"report_evolution_period_for_each\" name=\"report_evolution_period_for\" type=\"radio\" checked value=\"each\" ng-model=\"manageScheduledReport.report.evolutionPeriodFor\" />
                    <label for=\"report_evolution_period_for_each\" piwik-translate=\"ScheduledReports_EvolutionGraphsShowForEachInPeriod\">
                        <strong>::</strong>::";
        // line 103
        echo "{{ manageScheduledReport.getFrequencyPeriodSingle() }}";
        echo "
                    </label>
                </div>
                <div class=\"col s12\">
                    <input id=\"report_evolution_period_for_prev\" name=\"report_evolution_period_for\" type=\"radio\" value=\"prev\" ng-model=\"manageScheduledReport.report.evolutionPeriodFor\" />
                    <label for=\"report_evolution_period_for_prev\">
                        ";
        // line 109
        echo "{{ 'ScheduledReports_EvolutionGraphsShowForPreviousN'|translate:manageScheduledReport.getFrequencyPeriodPlural() }}";
        echo ":
                        <input type=\"number\" name=\"report_evolution_period_n\" ng-model=\"manageScheduledReport.report.evolutionPeriodN\" />
                    </label>
                </div>
            </div>
        </div>

        <div class=\"row\">
            <h3 class=\"col s12\">";
        // line 117
        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_ReportsIncluded")), "html", null, true);
        echo "</h3>
        </div>

        ";
        // line 120
        $context['_parent'] = $context;
        $context['_seq'] = twig_ensure_traversable(($context["reportsByCategoryByReportType"] ?? $this->getContext($context, "reportsByCategoryByReportType")));
        foreach ($context['_seq'] as $context["reportType"] => $context["reportsByCategory"]) {
            // line 121
            echo "            <div name='reportsList' class='row ";
            echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
            echo "'
                 ng-show=\"manageScheduledReport.report.type == '";
            // line 122
            echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
            echo "'\">

                ";
            // line 124
            if ($this->getAttribute(($context["allowMultipleReportsByReportType"] ?? $this->getContext($context, "allowMultipleReportsByReportType")), $context["reportType"], array(), "array")) {
                // line 125
                echo "                    ";
                $context["reportInputType"] = "checkbox";
                // line 126
                echo "                ";
            } else {
                // line 127
                echo "                    ";
                $context["reportInputType"] = "radio";
                // line 128
                echo "                ";
            }
            // line 129
            echo "
                ";
            // line 130
            $context["countCategory"] = 0;
            // line 131
            echo "
                ";
            // line 132
            $context["newColumnAfter"] = (int) floor(((twig_length_filter($this->env, $context["reportsByCategory"]) + 1) / 2));
            // line 133
            echo "
                <div class='col s12 m6'>
                    ";
            // line 135
            $context['_parent'] = $context;
            $context['_seq'] = twig_ensure_traversable($context["reportsByCategory"]);
            foreach ($context['_seq'] as $context["category"] => $context["reports"]) {
                // line 136
                echo "                    ";
                if (((($context["countCategory"] ?? $this->getContext($context, "countCategory")) >= ($context["newColumnAfter"] ?? $this->getContext($context, "newColumnAfter"))) && (($context["newColumnAfter"] ?? $this->getContext($context, "newColumnAfter")) != 0))) {
                    // line 137
                    echo "                    ";
                    $context["newColumnAfter"] = 0;
                    // line 138
                    echo "                </div>
                <div class='col s12 m6'>
                    ";
                }
                // line 141
                echo "                    <h3 class='reportCategory'>";
                echo \Piwik\piwik_escape_filter($this->env, $context["category"], "html", null, true);
                echo "</h3>
                    <ul class='listReports'>
                        ";
                // line 143
                $context['_parent'] = $context;
                $context['_seq'] = twig_ensure_traversable($context["reports"]);
                foreach ($context['_seq'] as $context["_key"] => $context["report"]) {
                    // line 144
                    echo "                            <li>
                                <input type='";
                    // line 145
                    echo \Piwik\piwik_escape_filter($this->env, ($context["reportInputType"] ?? $this->getContext($context, "reportInputType")), "html", null, true);
                    echo "' id=\"";
                    echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["report"], "uniqueId", array()), "html", null, true);
                    echo "\" report-unique-id='";
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["report"], "uniqueId", array()), "html", null, true);
                    echo "'
                                       name='";
                    // line 146
                    echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
                    echo "Reports'/>
                                <label for=\"";
                    // line 147
                    echo \Piwik\piwik_escape_filter($this->env, $context["reportType"], "html", null, true);
                    echo \Piwik\piwik_escape_filter($this->env, $this->getAttribute($context["report"], "uniqueId", array()), "html", null, true);
                    echo "\">
                                    ";
                    // line 148
                    echo call_user_func_array($this->env->getFilter('rawSafeDecoded')->getCallable(), array($this->getAttribute($context["report"], "name", array())));
                    echo "
                                    ";
                    // line 149
                    if (($this->getAttribute($context["report"], "uniqueId", array()) == "MultiSites_getAll")) {
                        // line 150
                        echo "                                        <div class=\"entityInlineHelp\">";
                        echo \Piwik\piwik_escape_filter($this->env, call_user_func_array($this->env->getFilter('translate')->getCallable(), array("ScheduledReports_ReportIncludeNWebsites", ($context["countWebsites"] ?? $this->getContext($context, "countWebsites")))), "html", null, true);
                        // line 151
                        echo "</div>
                                    ";
                    }
                    // line 153
                    echo "                                </label>
                            </li>
                        ";
                }
                $_parent = $context['_parent'];
                unset($context['_seq'], $context['_iterated'], $context['_key'], $context['report'], $context['_parent'], $context['loop']);
                $context = array_intersect_key($context, $_parent) + $_parent;
                // line 156
                echo "                        ";
                $context["countCategory"] = (($context["countCategory"] ?? $this->getContext($context, "countCategory")) + 1);
                // line 157
                echo "                    </ul>
                    <br/>
                    ";
            }
            $_parent = $context['_parent'];
            unset($context['_seq'], $context['_iterated'], $context['category'], $context['reports'], $context['_parent'], $context['loop']);
            $context = array_intersect_key($context, $_parent) + $_parent;
            // line 160
            echo "                </div>
            </div>
        ";
        }
        $_parent = $context['_parent'];
        unset($context['_seq'], $context['_iterated'], $context['reportType'], $context['reportsByCategory'], $context['_parent'], $context['loop']);
        $context = array_intersect_key($context, $_parent) + $_parent;
        // line 163
        echo "
        <input type=\"hidden\" id=\"report_idreport\" ng-model=\"manageScheduledReport.editingReportId\">

        <div ng-value=\"manageScheduledReport.saveButtonTitle\"
               onconfirm=\"manageScheduledReport.submitReport()\"
               piwik-save-button></div>

        <div class='entityCancel'>
            ";
        // line 171
        echo call_user_func_array($this->env->getFilter('translate')->getCallable(), array("General_OrCancel", "<a class='entityCancelLink' ng-click='manageScheduledReport.showListOfReports()'>", "</a>"));
        echo "
        </div>

    </form>
</div>
";
    }

    public function getTemplateName()
    {
        return "@ScheduledReports/_addReport.twig";
    }

    public function isTraitable()
    {
        return false;
    }

    public function getDebugInfo()
    {
        return array (  395 => 171,  385 => 163,  377 => 160,  369 => 157,  366 => 156,  358 => 153,  354 => 151,  351 => 150,  349 => 149,  345 => 148,  340 => 147,  336 => 146,  327 => 145,  324 => 144,  320 => 143,  314 => 141,  309 => 138,  306 => 137,  303 => 136,  299 => 135,  295 => 133,  293 => 132,  290 => 131,  288 => 130,  285 => 129,  282 => 128,  279 => 127,  276 => 126,  273 => 125,  271 => 124,  266 => 122,  261 => 121,  257 => 120,  251 => 117,  240 => 109,  231 => 103,  218 => 93,  208 => 86,  204 => 85,  196 => 80,  193 => 79,  184 => 76,  180 => 75,  176 => 74,  172 => 73,  168 => 72,  165 => 71,  161 => 70,  154 => 67,  150 => 66,  144 => 63,  140 => 61,  134 => 57,  132 => 56,  125 => 53,  121 => 52,  110 => 44,  105 => 42,  98 => 38,  93 => 36,  89 => 34,  82 => 30,  78 => 29,  69 => 24,  64 => 23,  60 => 22,  57 => 21,  55 => 20,  49 => 17,  44 => 15,  37 => 11,  32 => 9,  22 => 2,  19 => 1,);
    }

    /** @deprecated since 1.27 (to be removed in 2.0). Use getSourceContext() instead */
    public function getSource()
    {
        @trigger_error('The '.__METHOD__.' method is deprecated since version 1.27 and will be removed in 2.0. Use getSourceContext() instead.', E_USER_DEPRECATED);

        return $this->getSourceContext()->getCode();
    }

    public function getSourceContext()
    {
        return new Twig_Source("<div piwik-content-block
     content-title=\"{{ 'ScheduledReports_CreateAndScheduleReport'|translate|e('html_attr') }}\"
     class=\"entityAddContainer\"
     ng-if=\"manageScheduledReport.showReportForm\">
    <div class='clear'></div>
    <form id='addEditReport' piwik-form ng-submit=\"manageScheduledReport.submitReport()\">

        <div piwik-field uicontrol=\"text\" name=\"website\"
             title=\"{{ 'General_Website'|translate|e('html_attr') }}\"
             data-disabled=\"true\"
             value=\"{{ siteName|rawSafeDecoded }}\">
        </div>

        <div piwik-field uicontrol=\"textarea\" name=\"report_description\"
             title=\"{{ 'General_Description'|translate|e('html_attr') }}\"
             ng-model=\"manageScheduledReport.report.description\"
             inline-help=\"{{ 'ScheduledReports_DescriptionOnFirstPage'|translate|e('html_attr') }}\">
        </div>

        {% if segmentEditorActivated %}
            <div id=\"reportSegmentInlineHelp\" class=\"inline-help-node\">
                {% set SegmentEditor_DefaultAllVisits %}{{ 'SegmentEditor_DefaultAllVisits'|translate }}{% endset %}
                {% set SegmentEditor_AddNewSegment %}{{ 'SegmentEditor_AddNewSegment'|translate }}{% endset %}
                {{ 'ScheduledReports_Segment_Help'|translate('<a href=\"./\" rel=\"noreferrer noopener\" target=\"_blank\">','</a>',SegmentEditor_DefaultAllVisits,SegmentEditor_AddNewSegment)|raw }}
            </div>

            <div piwik-field uicontrol=\"select\" name=\"report_segment\"
                 ng-model=\"manageScheduledReport.report.idsegment\"
                 options=\"{{ savedSegmentsById|json_encode }}\"
                 title=\"{{ 'SegmentEditor_ChooseASegment'|translate|e('html_attr') }}\"
                 inline-help=\"#reportSegmentInlineHelp\">
            </div>
        {% endif %}

        <div id=\"emailScheduleInlineHelp\" class=\"inline-help-node\">
            {{ 'ScheduledReports_WeeklyScheduleHelp'|translate }}
            <br/>
            {{ 'ScheduledReports_MonthlyScheduleHelp'|translate }}
        </div>

        <div piwik-field uicontrol=\"select\" name=\"report_period\"
             options=\"{{ periods|json_encode }}\"
             ng-model=\"manageScheduledReport.report.period\"
             title=\"{{ 'ScheduledReports_EmailSchedule'|translate|e('html_attr') }}\"
             inline-help=\"#emailScheduleInlineHelp\">
        </div>

        <div piwik-field uicontrol=\"select\" name=\"report_hour\"
             options=\"manageScheduledReport.reportHours\"
             ng-change=\"manageScheduledReport.updateReportHourUtc()\"
             ng-model=\"manageScheduledReport.report.hour\"
             {% if timezoneOffset != 0 %}inline-help=\"#reportHourHelpText\"{% endif %}
             title=\"{{ 'ScheduledReports_ReportHour'|translate('X')|e('html_attr') }}\">
        </div>

        {% if timezoneOffset != 0 %}
            <div id=\"reportHourHelpText\" class=\"inline-help-node\">
                <span ng-bind=\"manageScheduledReport.report.hourUtc\"></span>
            </div>
        {% endif %}

        <div piwik-field uicontrol=\"select\" name=\"report_type\"
             options=\"{{ reportTypeOptions|json_encode }}\"
             ng-model=\"manageScheduledReport.report.type\"
             ng-change=\"manageScheduledReport.changedReportType()\"
             {% if reportTypes|length == 1 %}disabled=\"true\"{% endif %}
             title=\"{{ 'ScheduledReports_ReportType'|translate|e('html_attr') }}\">
        </div>

        {% for reportType, reportFormats in reportFormatsByReportTypeOptions %}
            <div piwik-field uicontrol=\"select\" name=\"report_format\"
                 class=\"{{ reportType }}\"
                 ng-model=\"manageScheduledReport.report.format{{ reportType }}\"
                 ng-show=\"manageScheduledReport.report.type == '{{ reportType }}'\"
                 options=\"{{ reportFormats|json_encode }}\"
                 title=\"{{ 'ScheduledReports_ReportFormat'|translate|e('html_attr') }}\">
            </div>
        {% endfor %}

        {{ postEvent(\"Template.reportParametersScheduledReports\") }}

        <div ng-show=\"manageScheduledReport.report.type == 'email' && manageScheduledReport.report.formatemail !== 'csv'\">
            <div piwik-field uicontrol=\"select\" name=\"display_format\" class=\"email\"
                 ng-model=\"manageScheduledReport.report.displayFormat\"
                 options=\"{{ displayFormats|json_encode }}\"
                 introduction=\"{{ 'ScheduledReports_AggregateReportsFormat'|translate|e('html_attr') }}\">
            </div>

            <div piwik-field uicontrol=\"checkbox\" name=\"report_evolution_graph\"
                 class=\"report_evolution_graph\"
                 ng-model=\"manageScheduledReport.report.evolutionGraph\"
                 ng-show=\"manageScheduledReport.report.displayFormat == '2' || manageScheduledReport.report.displayFormat == '3'\"
                 title=\"{{ 'ScheduledReports_EvolutionGraph'|translate(5)|e('html_attr') }}\">
            </div>

            <div
                class=\"row evolution-graph-period\"
                ng-show=\"manageScheduledReport.report.displayFormat == '1' || manageScheduledReport.report.displayFormat == '2' || manageScheduledReport.report.displayFormat == '3'\"
            >
                <div class=\"col s12\">
                    <input id=\"report_evolution_period_for_each\" name=\"report_evolution_period_for\" type=\"radio\" checked value=\"each\" ng-model=\"manageScheduledReport.report.evolutionPeriodFor\" />
                    <label for=\"report_evolution_period_for_each\" piwik-translate=\"ScheduledReports_EvolutionGraphsShowForEachInPeriod\">
                        <strong>::</strong>::{{ \"{{ manageScheduledReport.getFrequencyPeriodSingle() }}\" }}
                    </label>
                </div>
                <div class=\"col s12\">
                    <input id=\"report_evolution_period_for_prev\" name=\"report_evolution_period_for\" type=\"radio\" value=\"prev\" ng-model=\"manageScheduledReport.report.evolutionPeriodFor\" />
                    <label for=\"report_evolution_period_for_prev\">
                        {{ \"{{ 'ScheduledReports_EvolutionGraphsShowForPreviousN'|translate:manageScheduledReport.getFrequencyPeriodPlural() }}\" }}:
                        <input type=\"number\" name=\"report_evolution_period_n\" ng-model=\"manageScheduledReport.report.evolutionPeriodN\" />
                    </label>
                </div>
            </div>
        </div>

        <div class=\"row\">
            <h3 class=\"col s12\">{{ 'ScheduledReports_ReportsIncluded'|translate }}</h3>
        </div>

        {% for reportType, reportsByCategory in reportsByCategoryByReportType %}
            <div name='reportsList' class='row {{ reportType }}'
                 ng-show=\"manageScheduledReport.report.type == '{{ reportType }}'\">

                {% if allowMultipleReportsByReportType[reportType] %}
                    {% set reportInputType='checkbox' %}
                {% else %}
                    {% set reportInputType='radio' %}
                {% endif %}

                {% set countCategory=0 %}

                {% set newColumnAfter=(reportsByCategory|length + 1)//2 %}

                <div class='col s12 m6'>
                    {% for category, reports in reportsByCategory %}
                    {% if countCategory >= newColumnAfter and newColumnAfter != 0 %}
                    {% set newColumnAfter=0 %}
                </div>
                <div class='col s12 m6'>
                    {% endif %}
                    <h3 class='reportCategory'>{{ category }}</h3>
                    <ul class='listReports'>
                        {% for report in reports %}
                            <li>
                                <input type='{{ reportInputType }}' id=\"{{ reportType }}{{ report.uniqueId }}\" report-unique-id='{{ report.uniqueId }}'
                                       name='{{ reportType }}Reports'/>
                                <label for=\"{{ reportType }}{{ report.uniqueId }}\">
                                    {{ report.name|rawSafeDecoded }}
                                    {% if report.uniqueId=='MultiSites_getAll' %}
                                        <div class=\"entityInlineHelp\">{{ 'ScheduledReports_ReportIncludeNWebsites'|translate(countWebsites)
                                            }}</div>
                                    {% endif %}
                                </label>
                            </li>
                        {% endfor %}
                        {% set countCategory=countCategory+1 %}
                    </ul>
                    <br/>
                    {% endfor %}
                </div>
            </div>
        {% endfor %}

        <input type=\"hidden\" id=\"report_idreport\" ng-model=\"manageScheduledReport.editingReportId\">

        <div ng-value=\"manageScheduledReport.saveButtonTitle\"
               onconfirm=\"manageScheduledReport.submitReport()\"
               piwik-save-button></div>

        <div class='entityCancel'>
            {{ 'General_OrCancel'|translate(\"<a class='entityCancelLink' ng-click='manageScheduledReport.showListOfReports()'>\",\"</a>\")|raw }}
        </div>

    </form>
</div>
", "@ScheduledReports/_addReport.twig", "D:\\xampp\\htdocs\\matomo\\plugins\\ScheduledReports\\templates\\_addReport.twig");
    }
}
