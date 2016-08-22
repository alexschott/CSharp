$.widget("revilkent.polygonalGraphWidget", {
    options: {
        _id: "#myCanvas",
        _canvas: 0,
        _context: 0,
        grid: true,
        _class: "polygonalGraphWidget",
        _totPoints: 4,
        textFont: "normal 11px Arial",
        textColor: "#95acbc",
        circleLineWidth: 1,
        circleRadius: 90,
        circleLineColor: "rgba(232, 237, 240, 1)",
        circleBackgroundColor: "rgba(232, 237, 240, 1)",
        graph_colors: [
            "rgba(62, 107, 211, .8)"
        ],
        max_val:99,
        phase_start: (Math.PI / 3.5),
        margin: 36,
       
        labels: [],
        data: []
    },
    _create: function() {
        this.options = this._constrain(this.options);
        this.options._id = $(this.element).attr("id");
        this.element.addClass(this.options._class);
        this.options.canvas = document.getElementById(this.options._id);
        this.options.context = this.options.canvas.getContext("2d");
        this.refresh();
    },
    _setOption: function(key, value) {
        this._super(key, value);
    },
    _setOptions: function(options) {
        this._super(options);
        this.refresh();
    },
    refresh: function() {
        this.options.canvas.width = this.options.canvas.width;
        var context = this.options.context;
        try{
            this.options._totPoints = this.options.labels.length;
            if(this.options._totPoints < 3)
                throw "You must specify at least tree points!";
        }
        catch(err){
            this._error(err);
            return -1;
        }

        context.textAlign = "center";
     
        context.font = this.options.textFont;
        context.lineWidth = this.options.circleLineWidth;
        context.strokeStyle = this.options.circleLineColor;
        context.fillStyle = this.options.circleBackgroundColor;

        context.beginPath();
        var center_x = $(this.element).width() / 2;
        var center_y = $(this.element).height() / 2;
        var x = center_x - this.options.circleRadius, y = center_y - this.options.circleRadius;

        context.arc(center_x, center_y, this.options.circleRadius, 0, 2 * Math.PI, false);
        
        context.stroke();
        context.fill();
        //
        context.lineWidth = 2;
        context.fillStyle =  this.options.textColor;

        var arr = new Array();
        for(var i = 0; i < this.options._totPoints; i++)
            arr[i] = this.options.max_val + this.options.margin;
        p = new Polygonal(arr, {
            radius:         this.options.circleRadius,
            phase_start:    this.options.phase_start

        });
        p.calculatePoints();
         
        for (var i = 0; i < this.options._totPoints; i++) {
            context.fillText(this.options.labels[i], (p.points[i]['xScreen'] + x), (p.points[i]['yScreen'] + y));
            
        }
        if((this.options.data.length > 0)&&(typeof this.options.data[0] != 'object')){
            var data_temp = new Array(this.options.data);
            this.options.data = 0;
            this.options.data = data_temp;
        }

        

        var s;
        try {
            for (var i = 0; i < this.options.data.length; i++) {
                s = new Polygonal(this.options.data[i], {
                    radius: this.options.circleRadius,
                    phase_start: this.options.phase_start

                });
                s.calculatePoints();
                context.beginPath();
                context.strokeStyle = this.options.graph_colors[(i % 3)];
                context.moveTo((s.points[0]['xScreen'] + x), (s.points[0]['yScreen'] + y));

                context.fillStyle = this.options.graph_colors[(i % this.options.graph_colors.length)];
                if (s.points.length != this.options._totPoints) {
                    //this._error("Data and Labels must have the same number of elements");
                    throw "Labels and Data must have the same number of elements!";
                }

                for (var j = 0; j < this.options._totPoints; j++) {
                    context.lineTo((s.points[j]['xScreen'] + x), (s.points[j]['yScreen'] + y));
                }
                context.lineTo((s.points[0]['xScreen'] + x), (s.points[0]['yScreen'] + y));
                context.moveTo((s.points[0]['xScreen'] + x), (s.points[0]['yScreen'] + y));
                context.stroke();
                context.fill();

            }
        }
        catch (err) {
            this._error(err);
            return -1;
        }



        //-----------------------------------
        if (this.options.grid) {
            
            //this.options.max_val
            arr.length = 0;
            arr = 0;
            arr = new Array();
            for (var i = 0; i < this.options._totPoints; i++) {
                arr[i] = this.options.max_val;
            }
            l = new Polygonal(arr, {
                radius:         this.options.circleRadius,
                phase_start:    this.options.phase_start

            });


            //l1 = new Polygonal(arr, {
            //    radius: this.options.circleRadius,
            //    phase_start: this.options.phase_start

            //});
            
            l.calculatePoints();
            context.beginPath();
            context.strokeStyle = "rgba(51,58,71,1)";
            context.fillStyle = 'white';
            //context.shadowColor = '#e8edf0';
            context.shadowColor = 'white';
            context.shadowOffsetX = 2;
            context.shadowOffsetY = 2;
            context.shadowBlur = 1;
            context.lineWidth = .095;
            context.closePath();
            context.stroke();


            ///////////////////////////////// TEsting 
            //l1.calculatePoints();
            //context.beginPath();
            //context.strokeStyle = "rgba(221,228,231,.7)";
            //context.fillStyle = "#f53715";
            ////context.shadowColor = '#e8edf0';
            //context.shadowColor = '#000';
            //context.shadowOffsetX = 2;
            //context.shadowOffsetY = 3;
            //context.shadowBlur = 5;
            //context.lineWidth = 1;
            //context.stroke();



            //context.fillStyle = 'red';
            //context.shadowColor = '#629ab2';
            //context.shadowBlur = 6;
            //context.shadowOffsetX = 3;
            //context.shadowOffsetY = 4;
         
            //context.fill();

            ///////////////////////////////////////////////


           ////////////////////////////////////////////// //context.op
            for(var i = 0; i < l.points.length; i++){
                context.moveTo(center_x, center_y);
                context.lineTo((l.points[i]['xScreen'] + x), (l.points[i]['yScreen'] + y));
                //alert((l.points[i]['xScreen'] + x )+ '--' + (l.points[i]['yScreen']+y))
            }

            //for (var i = 0; i < l1.points.length; i++) {
            //    context.moveTo(center_x, center_y);
            //    context.lineTo((l1.points[i]['xScreen'] + x), (l1.points[i]['yScreen'] + y));
            //    //alert((l.points[i]['xScreen'] + x )+ '--' + (l.points[i]['yScreen']+y))
            //}



            
            context.stroke();
         

            // making circles
            for (var i = 0; i < l.points.length; i++) {
                context.beginPath();
                context.lineWidth = 2;
                context.fillStyle = "#fff";
                var lname=this.options.labels[i];
                //alert(lname)
                if (lname == "New Deals") context.strokeStyle = '#13912f';
                else if (lname == "Completed Deals") context.strokeStyle = '#9052de';
                else if (lname == "Productivity") context.strokeStyle = '#3653d8';
                else if (lname == "Cancelled Deals") context.strokeStyle = '#be4425';
                else if (lname == "Deals Waiting on Clients") context.strokeStyle = '#e5c651';
                else  context.strokeStyle = '#13912f';
                //context.strokeStyle = 'red';
                context.shadowOffsetX = 1;
                context.shadowOffsetY =0;
                context.shadowBlur = 5;
                context.shadowColor = '#fff';
                
                context.arc((l.points[i]['xScreen'] + x), (l.points[i]['yScreen'] + y), 4, 0, 2 * Math.PI);
                context.fill();
                context.closePath();
                context.stroke();
                
          
            }
            
        }




     
       

      
         






    },
    _constrain: function(options) {
        /*
        * add constraint
        * */
        return options;
    },
    _error: function(message, fileName, lineNumber) {
        alert('ERROR: ' + message);
        return new Error(message, fileName, lineNumber);
    },
    _destroy: function() {
        this.element.removeClass(this.options._class).text("");
    }

});
//----------------------------------------------------------------------------------------------------------------------
/**
 * CLASS Coordinates
 **/
function Coordinates(args) {
    this.radius = 0;
    this.x = 0;
    this.y = 0;
    this.module = 0;
    this.phase = 0;
    this.xScreen = 0;
    this.yScreen = 0;
    this.__construct(args);
    return this;
}
 






 














 

Coordinates.prototype.__construct = function(args) {
    for(var i in args)
        this[i] = args[i];
};

Coordinates.prototype.polarToCartesian = function() {
    this.x = this.module * Math.cos(this.phase);
    this.y = this.module * (-Math.sin(this.phase));
    return this;
};

Coordinates.prototype.cartesianToScreen = function() {
    this.xScreen = Math.ceil(this.x + this.radius);
    this.yScreen = Math.ceil(this.y + this.radius);
    return this;
};
//----------------------------------------------------------------------------------------------------------------------
/**
 * CLASS Polygonal
 **/

function Polygonal(data, args) {
    this.radius = 150;
    this.phase_start = 0;
    this.max_val = 99;
    this.points = [];
    this.data = [];
    this.__construct(data, args);
    return this;
}

Polygonal.prototype.__construct = function(data, args) {
    this.data = data;
    if(args == null) args = new Object();
    for(var i in args)
        this[i] = args[i];
}


Polygonal.prototype.calculatePoints = function() {
    var num_points = this.data.length;
    degree = (2 * Math.PI) / num_points;
    for(var i = 0 ; i < num_points; i++){
        var module = (this.data[i] / this.max_val) * this.radius;
        var phase = this.phase_start + (degree * i);
        var c = new Coordinates({
            radius: this.radius,
            module: module,
            phase: phase
        });

        c.polarToCartesian().cartesianToScreen();
        this.points[i] = this.getScreenPoint(c);
        
    }
    return this.points;
}

Polygonal.prototype.getScreenPoint = function (c) {
    
    return {
        xScreen: c.xScreen,
        yScreen: c.yScreen
    }
};




