var HomeClient = new function () {
    this.OnLogOnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.LOGIN_SUCCESS);
            $('#login-form').modal('hide');

            setTimeout(function () { location.reload(); }, 3000);
        } else {
            
            Feedback.ShowError(response.Data.Error);

        }
    };

    this.OnLogOnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };
    this.OnRegisterSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.REGISTER_SUCCESS);
            setTimeout(function () { location.reload(); }, 3000);
            
            $('#signup-form').modal('hide');
        } else {
            Feedback.ShowError(response.Data.Error);

        }
    };

    this.OnRegisterFailed = function (jsonData) {
        console.log(1);
        Feedback.ShowError(jsonData);
    };
    
}