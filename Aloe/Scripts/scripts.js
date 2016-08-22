

$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})



jQuery(document).ready(function ($) {
	$('#tabs').tab();
});


$(document).ready(function () {
			
			$(".date-picker").datepicker();
			$(".date-picker").on("change", function () {
				var id = $(this).attr("id");
				var val = $("label[for='" + id + "']").text();
				$("#msg").text(val + " changed");
			}); 
		
		});
		
		
		
		

$(".choice").change(function () {
    if($(this).val() == "0") $(this).addClass("empty");
    else $(this).removeClass("empty")
});

$(".choice").change();




//left side
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});



//right side
$("#menu-toggle1").click(function (e) {
	e.preventDefault();
    $("#wrapper").toggleClass("toggled1");
});

