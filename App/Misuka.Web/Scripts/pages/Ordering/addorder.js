var OrderClient = new function () {
    
    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.DATA_SAVED_SUCCESS);
            if ($('#OrderId').val() == '0') {
                $('#OrderId').val(response.Data.OrderId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };


};