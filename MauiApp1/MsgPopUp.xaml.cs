using CommunityToolkit.Maui.Views;

namespace MauiApp1;

public partial class MsgPopUp : Popup {
	public MsgPopUp(string message, string confirmText = "Yes", string cancleText = "Cancle")
	{
		InitializeComponent();

		BindingContext = new MsgPopUpTexts() {
			Message = message,
			ConfirmText = confirmText,
			CancleText = cancleText
		};
	}



	internal class MsgPopUpTexts {
		public string Message { get; set; }
		public string ConfirmText { get; set; }
		public string CancleText { get; set; }
	}

	private void BtnOk_Clicked(object sender, EventArgs e) => Close(true);

	private void BtnCancle_Clicked(object sender, EventArgs e) => Close(false);
}