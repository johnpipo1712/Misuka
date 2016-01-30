var OrderClient = new function () {
    
    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.DATA_SAVED_SUCCESS);
          
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };


};