var OrderClient = new function () {
    
    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.DATA_SAVED_SUCCESS);
          
        } else {
            
            Feedback.ShowError(response.Data.Error);

        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };
    this.DropDownListExchangeRates = function () {
        $("#ExchangeRateId").kendoDropDownList({
            filter: "contains",
            dataTextField: "text",
            dataValueField: "value",
            type: "json",
            dataSource: {
                transport: {
                    read: {
                        dataType: "json",
                        url: '/DropDownList/GetExchangeRates',
                    }
                }
            },
            index: 0,
           
        });
    };

};