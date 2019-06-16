(function($) {
	$(document).ready(function() {
		
		var updateCodeMirror = function(data){
		    var cm = $('.CodeMirror')[0].CodeMirror;
		    var doc = cm.getDoc();
		    var cursor = doc.getCursor(); // gets the line number in the cursor position
		    var line = doc.getLine(cursor.line); // get the line contents
		    var pos = { // create a new object to avoid mutation of the original selection
		        line: cursor.line,
		        ch: line.length - 1 // set the character position to the end of the line
		    }
		    doc.replaceRange('\n'+data+'\n', pos); // adds a new line
		}

		var validateForm = function(form){
			$(form).parsley();
	    	$(form).parsley().validate();
	    	return $(form).parsley().isValid();
		}
		
		/*$(document).on('shown.bs.modal', function() {
		  
		});*/
		$(document).on('click','.emmaFeatureStepAddPopUpTiger', function(e){
			var opTarget = $(this).data('op');
			var parmTarget = $(this).data('parm');
			var basearget = $(this).data('base');

			$('#dinaOps').text(opTarget);
			$('#ModelOpsInput').val(parmTarget);

			if(projetcTypeGlb === 'NodeJs') {
				$('#functionCallNMTarget').hide();
			}
			else {
				$('#functionCallNMTarget').show();
			}
			$('#newEmmaStepDefModel').modal('show');
		});

		$(document).on('click','#addNewEmmaStepTigger', function(e){
			var outData = '';
	        if(projetcTypeGlb === 'NodeJs') {
	        	outData = ''+$('#dinaOps').text()+'(/^'+$('#ModelOpsInput').val()+'$/, ('+$('#ParamsInput').val().replace(/\n/g , ",")+') => { \n \n });';
	      	}
	      	else {
	        	outData = '';
	      	}
	      	updateCodeMirror(outData);
	      	$('#newEmmaStepDefModel').modal('hide');
		});


		$(document).on('click','.emmaFeatureItemRemoveTig', function(e){
	    	var rowId = $(this).data('load');
	    	var form = document.createElement("form");
			var hiddeElement = document.createElement("input"); //input element, text
			hiddeElement.setAttribute('type',"hidden");
			hiddeElement.setAttribute('name',"emmaFeatureId");
			hiddeElement.setAttribute('value',rowId);
			form.appendChild(hiddeElement);
			var values = $(form).serialize();
			$.ajax({
                url: "/removeFeature",
                type: "post",
                data: values,
                success: function(data) {
                    if(data.Status == 'Ok'){
                    	$.toast({
					      heading: 'Sucess',
					      text: 'Data removed sucessfully',
					      showHideTransition: 'slide',
					      icon: 'success',
					      loaderBg: '#f96868',
					      position: 'top-right'
					    });
					    $('#newEmmaFeatureModel').modal('hide');
					    location.reload();
                    }
                    else{
                    	$.toast({
					      heading: 'Error',
					      text: data.Message,
					      showHideTransition: 'slide',
					      icon: 'error',
					      loaderBg: '#f2a654',
					      position: 'top-right'
					    });
                    }
                }
            });

	    });

		$(document).on('shown.bs.modal', '#newEmmaFeatureModel', function() {
			if(editor){
				editor.refresh();
			}
		});//bind(editor));
 		
 		$(document).on('click','#addNewEmmaFeatureTigger', function(e){


 			$('#emmaFeatureStepDescInput').val(editor.getValue().trim());

	 	   	if(!validateForm($('#emmaFeatureForm'))){
	    		 resetToastPosition();
				    $.toast({
				      heading: 'Invalid Ops',
				      text: 'Please provide all information',
				      showHideTransition: 'slide',
				      icon: 'error',
				      loaderBg: '#f2a654',
				      position: 'top-right'
				    });
				    return;
	    	}

	    	var values = $('#emmaFeatureForm').serialize();
	    	$.ajax({
                url: "/createFeature",
                type: "post",
                data: values,
                success: function(data) {
                    if(data.Status == 'Ok'){
                    	$.toast({
					      heading: 'Sucess',
					      text: 'Data updated sucessfully',
					      showHideTransition: 'slide',
					      icon: 'success',
					      loaderBg: '#f96868',
					      position: 'top-right'
					    });
					    $('#newEmmaFeatureModel').modal('hide');
					    location.reload();
                    }
                    else{
                    	$.toast({
					      heading: 'Error',
					      text: data.Message,
					      showHideTransition: 'slide',
					      icon: 'error',
					      loaderBg: '#f2a654',
					      position: 'top-right'
					    });
                    }
                }
            });

	    });
		 
	    $(document).on('click','.emmaProjectItemRemoveTig', function(e){
	    	var rowId = $(this).data('load');
	    	var form = document.createElement("form");
			var hiddeElement = document.createElement("input"); //input element, text
			hiddeElement.setAttribute('type',"hidden");
			hiddeElement.setAttribute('name',"emmaProjectId");
			hiddeElement.setAttribute('value',rowId);
			form.appendChild(hiddeElement);
			var values = $(form).serialize();
			$.ajax({
                url: "/removeProject",
                type: "post",
                data: values,
                success: function(data) {
                    if(data.Status == 'Ok'){
                    	$.toast({
					      heading: 'Sucess',
					      text: 'Data removed sucessfully',
					      showHideTransition: 'slide',
					      icon: 'success',
					      loaderBg: '#f96868',
					      position: 'top-right'
					    });
					    $('#newProjectModel').modal('hide');
					    location.reload();
                    }
                    else{
                    	$.toast({
					      heading: 'Error',
					      text: data.Message,
					      showHideTransition: 'slide',
					      icon: 'error',
					      loaderBg: '#f2a654',
					      position: 'top-right'
					    });
                    }
                }
            });

	    });

	    $(document).on('click','#addNewEmmaProjectTigger', function(e){

	 	   	if(!validateForm($('#emmaProjectForm'))){
	    		 resetToastPosition();
				    $.toast({
				      heading: 'Invalid Ops',
				      text: 'Please provide all information',
				      showHideTransition: 'slide',
				      icon: 'error',
				      loaderBg: '#f2a654',
				      position: 'top-right'
				    });
				    return;
	    	}

	    	var values = $('#emmaProjectForm').serialize();
	    	$.ajax({
                url: "/createProject",
                type: "post",
                data: values,
                success: function(data) {
                    if(data.Status == 'Ok'){
                    	$.toast({
					      heading: 'Sucess',
					      text: 'Data updated sucessfully',
					      showHideTransition: 'slide',
					      icon: 'success',
					      loaderBg: '#f96868',
					      position: 'top-right'
					    });
					    $('#newProjectModel').modal('hide');
					    location.reload();
                    }
                    else{
                    	$.toast({
					      heading: 'Error',
					      text: data.Message,
					      showHideTransition: 'slide',
					      icon: 'error',
					      loaderBg: '#f2a654',
					      position: 'top-right'
					    });
                    }
                }
            });




	    });

	});
})(jQuery);