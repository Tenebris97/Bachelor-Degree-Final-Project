$((function(){
    var url;
    var redirectUrl;
    var target;

    $('body').append(`
            <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">اخطار</h4>
                </div>
                <div class="modal-body delete-modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="cancel-delete">بازگشت</button>
                    <button type="button" class="btn btn-danger" id="confirm-delete">حذف</button>
                </div>
                </div>
            </div>
            </div>`);

    //Delete Action
    $(".delete").on('click',(e)=>{
        e.preventDefault();

        target = e.target;
        var Id = $(target).data('id');
        var controller = $(target).data('controller');
        var action = $(target).data('action');
        var bodyMessage = $(target).data('body-message');
        var area = $(target).data('area');     
        var item = $(target).data('item');
        redirectUrl = $(target).data('redirect-url');

        url = "/"+area+"/"+controller+"/"+action+"/"+Id;
        $(".delete-modal-body").text(bodyMessage);
        $("#deleteModal").modal('show');
    });

    $("#confirm-delete").on('click',()=>{
        $.get(url)
            .done((result)=>{
                swal("عملیات موفق", $(target).data('item') + " " + "با موفقیت حذف شد","success");

                if(!redirectUrl){
                    return $(target).parent().parent().hide("slow");
                }
                window.location.href = redirectUrl;                    
            })
            .fail((error)=>{ 
                swal("عملیات ناموفق","خطایی رخ داده است","error");

                if(redirectUrl)             
                    window.location.href = redirectUrl;
            }).always(()=>{
                $("#deleteModal").modal('hide');                    
            });
    });

}()));