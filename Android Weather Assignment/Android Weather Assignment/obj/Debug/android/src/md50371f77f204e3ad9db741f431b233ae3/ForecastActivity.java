package md50371f77f204e3ad9db741f431b233ae3;


public class ForecastActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Android_Weather_Assignment.ForecastActivity, Android Weather Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ForecastActivity.class, __md_methods);
	}


	public ForecastActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ForecastActivity.class)
			mono.android.TypeManager.Activate ("Android_Weather_Assignment.ForecastActivity, Android Weather Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
