// JavaScript Document
// $(document).ready
//(
//	function ()
//	{
//		var btn = $("#t1 input");
//		btn.tooltip();
//		$("#toolthis").tooltip();
//		$("#changeval").click
//		(
//			function ()
//			{
//				$("#posiciones").attr("value", 500);
//			}
//		);
//	}
//);
	function hola()	 {	alert("Hola");	} //onLoad
	function adios() {	alert("Adios");	} //onUnLoad
	function pulsa() {	alert("EL autor es Mike");	}
	function foco()	 {	alert("foco en la 1ra caja");	}
	function tecla() {	alert("pulsada tecla");	}
	function cambio() {	alert("cambio de tamano");	} //onResize
	
	function valor()
	{
		var nombre;
		nombre = prompt("introduce tu nombre");
		if(nombre != null || nombre== "" )				
			alert("hola "+ nombre);						
		else
			alert("no tecleó nada!!");
	}
	
	function ver() 
	{
		var edad = parseInt(txtEdad.value);
		if(edad<=18)
			alert("No tienes acceso");
		else
			alert("Bienvenido");	
	}
	
	function eliminaNotificaciones()
	{
		eliminaArea("logSuccess");
		eliminaArea("logInfo");
		eliminaArea("logWarning");
		eliminaArea("logDanger");
	} 
	
	function eliminaArea(area)
	{
		var node = document.getElementById(area);
		while (node.hasChildNodes()) 
		{
			node.removeChild(node.lastChild);
		}
	}
				
	function success()
	{
		if(document.getElementById('cbTodos').checked == true)
			eliminaNotificaciones();
		else
			eliminaArea("logSuccess");										 						
		var html;
		html = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
		"<button type='button' class='close' data-dismiss='alert'>×</button>" +
		"<strong>SUCCESS </strong> Todo salió bien!.</div>";	
		$("#logSuccess").append(html);			
	}
	
	function info() 
	{				
		if(document.getElementById('cbTodos').checked == true)
			eliminaNotificaciones();
		else
			eliminaArea("logInfo");	
		var html;
		html = "<div class='alert alert-info' style='margin-bottom: 5px;'>" +
		"<button type='button' class='close' data-dismiss='alert'>×</button>" +
		"<strong>INFO </strong>Algo puede estar pasando, revisa el código.</div>";	
		$("#logInfo").append(html);						
	}
	
	function warning() 
	{				
		if(document.getElementById('cbTodos').checked == true)
			eliminaNotificaciones();
		else
			eliminaArea("logWarning");	
		var html;
		html = "<div class='alert alert-warning' style='margin-bottom: 5px;'>" +
		"<button type='button' class='close' data-dismiss='alert'>×</button>" +
		"<strong>WARNING </strong>Algún componente no cargó bien, revísalo.</div>";	
		$("#logWarning").append(html);						
	}
	
	function danger() 
	{				
		if(document.getElementById('cbTodos').checked == true)
			eliminaNotificaciones();
		else
			eliminaArea("logDanger");	
		var html;
		html = "<div class='alert alert-danger' style='margin-bottom: 5px;'>" +
		"<button type='button' class='close' data-dismiss='alert'>×</button>" +
		"<strong>DANGER </strong>Daño en el sistema, explotarás en 5..4..3..2..1.. <strong>BOOM!!</strong>.</div>";	
		$("#logDanger").append(html);						
	}
	
	function changeImage() 
	{
		var image = document.getElementById('myImage');
		if (image.src.match("bulbon")) 
			image.src = "images/pic_bulboff.gif";
		else 
			image.src = "images/pic_bulbon.gif";
    }
			
	function returnDate()
	{
		var fecha = Date();	
		document.getElementById("labelDemo").innerHTML = Date();	
	}
	
	function myFunction() 
	{
		var x, text;	
		// Get the value of the input field with id="numb"
		x = document.getElementById("numb").value;	
		// If x is Not a Number or less than one or greater than 10
		if (isNaN(x) || x < 1 || x > 10) {
			text = "Input not valid";
		} else {
			text = "Input OK";
		}
		document.getElementById("demo").innerHTML = text;
	}