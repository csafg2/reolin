    
       

$("document").ready(function () {
   
    $('#btnleft').click(function () {
        if ($('#pushy').is('.pushy-open')) {
            $("#btnleft p.arrowright").removeClass("arrowright");
            $("#btnleft p").addClass("arrowleft");
            $("#btnleft").removeClass("menu-btnopen");

        }
        else {
            $("#btnleft p").removeClass("arrowleft");
            $("#btnleft p").addClass("arrowright ");
            $("#btnleft").addClass("menu-btnopen");
        }
 
    });

    $('[data-search-open]').click(function () {
        var openmenu = $(this).data('search-open');
        $('#btnleft p').removeClass('arrowleft');
        $('#btnleft p').addClass('arrowright');
        $('#pushy').removeClass('pushy-left');
        $('#pushy').addClass('pushy-open');
        $("#btnleft").addClass("menu-btnopen");
        $('#btnrightt p').removeClass('arrowright');       
        $('#btnrightt p').addClass('arrowleft');
        $('#pushyright').removeClass('pushy-right');
        $('#pushyright').addClass('pushy-open-right');
        $('#btnrightt').addClass('menu-btn-rightopen');

    });

    $('[data-category]').keypress(function (event) {
        if (event.keyCode == 13) {
            $('[data-search-open]').click();
        }
    });
});

$("document").ready(function () {

    $('#btnrightt').click(function () {
        if ($('#pushyright').is('.pushy-open-right')) {

            $("#btnrightt p.arrowleft").removeClass("arrowleft");
            $("#btnrightt p").addClass("arrowright");
            $("#btnrightt").removeClass("menu-btn-rightopen");
            $("#shiftheader").removeClass("headershift");
            $(".sub-menu").css('right', '');
            $(".sub-menu").css('top', '');



        }
        else {
            $("#btnrightt p").removeClass("arrowright");
            $("#btnrightt p").addClass("arrowleft ");
            $("#btnrightt").addClass("menu-btn-rightopen");
            $("#shiftheader").addClass("headershift");
            $(".sub-menu").css('right', '-5px');
            $(".sub-menu").css('top', '19px');



        }
  
    });
});
    $(function () {
        $('pushy').scroll(function () {
            if ($(this).scroll() > 40) {

                $("#leftside-category").addClass('leftside-category');

            }
            else {
                $("#leftside-category").removeClass('leftside-category');

            }

        });
    });
    $(function () {
        $('#likebtn').click(function () {
            $('#pushy').addClass('pushy-open');
            $('#pushy').removeClass('pushy-left');

        });           
    }); 
    $("document").ready(function () {
        $('.has-submenu').mouseover(function () {           
            $(".sub-menu").addClass("sub-menu-open");
        });
        $('.has-submenu').mouseleave(function () {  
            $(".sub-menu").removeClass("sub-menu-open");
        });
               
          
            
    });

///share a place 
    $("document").ready(function () {
        $('ul[data-palceitem]').hide();
        $("a[data-placeparent]").click(function () {
            var placeid = $(this).data("placeparent");
            $('ul[data-palceitem]').toggle();
            $('ul[data-palceitem]').not("[data-palceitem=" + placeid + "]").hide();
        });
    });
    $("document").ready(function () {     
        $("[data-editrelatebtn]").click(function () {
            var editrelated = $(this).data("editrelatebtn");
            $('div[data-editrelated]').toggle();
            $('div[data-editrelated]').not("[data-editrelated=" + editrelated + "]").hide();
        });
    });

////////////////////left-side-menu 
    $("document").ready(function () {
        $('ul.tabs-rightside li a').click(function () {

            var category = $(this).attr('data-tab');
            if (category == 'Distance')
            {
                $('.secondul ul li a').addClass('disable-a');
            }
            if (category == 'Popularity')
            {
                $('.secondul ul li a').removeClass('disable-a');
                category = 'city';
              
            }
        

                $('ul.tabs-rightside li a').removeClass('current-link');
                $('.tab-content').removeClass('current-content');
                if (category == 'Country') {
                    $('#Popularity').addClass('current-link');


                }
                if (category == 'Global') {
                    $('#Popularity').addClass('current-link');


                }
                if (category == 'city') {
                    $('#Popularity').addClass('current-link');


                }
                $(this).addClass('current-link');
                if (category == 'city')
                {
                    $('#cityfirst').addClass('current-link')
                }
                $("#" + category).addClass('current-content');
           
        })
        $('a[data-tab-suggest]').click(function () {
            var suggesttab = $(this).data('tab-suggest');
            $('[data-tab-suggest]').removeClass('suggest-tablink-active'); 
            $('[data-suggest-content]').removeClass('suggest-tab-active');
            $(this).addClass('suggest-tablink-active');
            $('div[data-suggest-content="' + suggesttab + '"]').addClass('suggest-tab-active');
        })
        $('#comment-btn').click(function () {
            var name = $('#comment-name').val();
            var email = $('#comment-email').val();
            var text = $('#comment-text').val();
            if(name.length==0)
            {
                $('#comment-name').next().css('display','block');
            }
            if (name.length > 0) {
                $('#comment-name').next().css('display', '');
            }
            if (email.length == 0) {
                $('#comment-email').next().css('display','block');
            }
            if (email.length > 0) {
                $('#comment-email').next().css('display', '');
            }
            if (text.length == 0) {
                $('#comment-text').next().css('display','block');
            }
            if (text.length > 0) {
                $('#comment-text').next().css('display', '');
            }
        })

    });

  

    $("document").ready(function () {
        $('.has-submenu-edit').mouseover(function () {
            $(".sub-menu-edit").addClass("edit-open");
        });
        $('.has-submenu-edit').mouseleave(function () {
            
            $(".sub-menu-edit").removeClass("edit-open");
            
        });



    });
    $("document").ready(function () {
        $('#openmngimg').click(function () {
            $(".group-picture").toggle();
        });
        $('#sethashtag').click(function () {
            $(".group-hashtag").toggle();
        });
        $('.cancel-mngimg').click(function () {
            $(".group-picture").hide();
        });
        $('#plusimage').click(function () {
            $(".add-picture-body").toggle();
        });
        $('#plusnetwork').click(function () {
            $(".add-network-body").toggle();
        });


        
    });
   
    $("document").ready(function () {
        $('.edit-icon').click(function () {
            var itemedit = $(this).attr('data-edit');          
            $("#" + itemedit).show();
            $('.cancel').click(function () {
                $("#" + itemedit).hide();
            })
        })

        //suggest page --share btn and show and more text 
        var myTag = $('.show-less').text();
        if (myTag.length > 5) {
            var truncated = myTag.trim().substring(0, 65) + "…";
            $('.show-less').text(truncated);
        }     
       
        $(document.body).on('click', '[data-more]', function () {
            var showmore = $(this).data('more');
            $('div[data-place="' + showmore + '"]').css('height', '496px');                      
            var showtext = myTag.trim().substring(0, 600);
            $('p[data-text="' + showmore + '"]').text(showtext);           
            $(this).removeClass('fa-angle-down');
            $(this).replaceWith("<i class=\"fa fa-angle-up\"   data-less=\""+showmore+"\"></i>");                               
        });
        $(document.body).on('click', '[data-less]', function () {
            var showless = $(this).data('less');
            $('div[data-place="' + showless + '"]').css('height', '');
            var showtext = myTag.trim().substring(0, 65);
            $('p[data-text="' + showless + '"]').text(showtext);
            $(this).removeClass('fa-angle-up');
            $(this).replaceWith("<i class=\"fa fa-angle-down\"   data-more=\"" + showless + "\"></i>");
        });
        $('ul[data-shareitem]').hide();
        $('a[data-sharebtn]').click(function () {
            var social = $(this).data('sharebtn');
            $('ul[data-shareitem="' + social + '"]').toggle();

        });
        $('a[data-tabsuggest]').click(function () {
            var suggest = $(this).data('tabsuggest');
            $('a[data-tabsuggest]').removeClass('suggest-link-active');
            $('div[data-suggest]').removeClass('suggest-category-active');
            $(this).addClass('suggest-link-active');
            $('div[data-suggest="' + suggest + '"]').addClass('suggest-category-active');



        });
  
        //edit item
             kk = 0
            $('i[data-editbtn]').click(function () {
                var editbtn = $(this).data('editbtn');
                if (kk > 0) {
                    //$('div[data-editform="' + editbtn + '"]').css('display' , '');
                    $('div[data-editform="' + editbtn + '"]').removeClass('edit-from-open');
                }
                else
                {
                    //$('div[data-editform="' + editbtn + '"]').css('display', 'block');
                    $('div[data-editform="' + editbtn + '"]').addClass('edit-from-open');
                }
                if (kk > 0) {
                    kk = 0;
                }
                else {
                    kk = 1;
                }
                $('.close-form').click(function () {
                    $('div[data-editform="' + editbtn + '"]').removeClass('edit-from-open');
                    //$('div[data-editform="' + editbtn + '"]').css('display', '');
                });
            });
            kk = 0
            $('a[data-awardbtn]').click(function () {
                var award = $(this).data('awardbtn');
                if (kk > 0)
                    {
                    $('div[data-award="' + award + '"]').removeClass('edit-award-form-open');
                }
                else
                {
                    $('div[data-award="' + award + '"]').addClass('edit-award-form-open');
                }


                if (kk > 0) {
                    kk = 0;
                }
                else {
                    kk = 1;
                }
            });
      
        /////////////////////

            $(document.body).on('click', '[data-servicebtn]', function () {
                var service = $(this).data('servicebtn');
                var servicename = $('p[data-servicename="'+service+'"]').text();        
                $(this).removeClass('fa-edit');
                $(this).replaceWith("<i class=\"fa fa-check\"   data-checked=\""+service+"\"></i>");
                $('p[data-servicename="' + service + '"]').replaceWith("<input id=\"Text1\" class=\"dynamic-edit\" data-input=\"" + service + "\" type=\"text\"  /> ");
                $('[data-input="' + service + '"]').val("" + servicename + "");
            });

        //////////////////////////////////////////////
            $(document.body).on('click', '[data-grouppic-edit]', function () {
                var service = $(this).data('grouppic-edit');
                var servicename = $('td[data-grouppic="' + service + '"]').text();
                $(this).removeClass('fa-edit');
                $(this).replaceWith("<i class=\"fa fa-check\"   data-grouppic-checked=\"" + service + "\"></i>");
                $('td[data-grouppic="' + service + '"]').replaceWith("<input id=\"Text1\" class=\"dynamic-edit-pic\" data-input=\""+service+"\" type=\"text\"  /> ");
                $('[data-input="'+service+'"]').val(""+servicename +"");
            });
            $(document.body).on('click', '[data-grouppic-checked]', function () {
                var service = $(this).data('grouppic-checked');
                var newvalue = $('[data-input="' + service + '"]').val();
                $(this).removeClass('fa-checked');
                $(this).replaceWith("<i class=\"fa fa-edit\"   data-grouppic-edit=\"" + service + "\"></i>");
                $('[data-input="' + service + '"]').replaceWith("<td class=\"item-add-group\" data-grouppic=" + service + ">" + newvalue + "</td> ");


            });

             

        ////////////////////////

            $(document.body).on('click', '[data-actionedit]', function () {
                var item = $(this).data('actionedit');
                $('[data-edititem="' + item + '"]').css('display', 'table-row');

            });
            $(document.body).on('click', '[data-actionconfirm]', function () {
                var item = $(this).data('actionconfirm');
                $('[data-edititem="' + item + '"]').css('display', '');

            });


        /////// select category in the main search box/////////////////
            $(document.body).on('click', '.select-cat a', function () {
                var maincategory = $(this).text();
                $('[data-category]').addClass('search-input');
                $('[data-category]').val("" + maincategory + " | ");
               
            });





       /////////////////////////    
            $(document.body).on('click', '[data-checked]', function () {                                                                                     
                   var service = $(this).data('checked');
                   var newvalue = $('[data-input="' + service + '"]').val();                   
                    $(this).removeClass('fa-checked');
                    $(this).replaceWith("<i class=\"fa fa-edit\"   data-servicebtn=\"" + service + "\"></i>");
                    $('[data-input="' + service + '"]').replaceWith("<p data-servicename="+service+">" + newvalue + "</p> ");
                  
                  
            });
     
            $(function () {
                $(window).scroll(function () {
                    if ($(this).scrollTop() > 40) {

                        $("#affixtop").addClass('affixtop');
                        $("#affixtop").addClass('fadeInDown');
                        $("#affixleft").addClass('affixleft');
                        $("#affixright").addClass('affixright');
                    }
                    else {
                        $("#affixtop").removeClass('affixtop');
                        $("#affixtop").removeClass('fadeInDown');
                        $("#affixleft").removeClass('affixleft');
                        $("#affixright").removeClass('affixright');
                    }

                });
            });
            
    

    });
    