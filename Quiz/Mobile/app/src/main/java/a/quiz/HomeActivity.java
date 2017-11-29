package a.quiz;

import android.Manifest;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.icu.text.UnicodeSetSpanner;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import java.io.Console;
import java.security.Permission;

public class HomeActivity extends AppCompatActivity
{
    Question[] questionArray;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);



        // Start button
        Button btnStart = (Button) findViewById(R.id.btnStart);
        btnStart.setOnClickListener(new BtnStartClickHandler());


        // Assume thisActivity is the current activity
        int permissionCheck = ContextCompat.checkSelfPermission(this,
                Manifest.permission.INTERNET);

        perm();

        ActivityCompat.requestPermissions(this,   new String[]{Manifest.permission.INTERNET}, 0);

    }


    // Start Button
    public class BtnStartClickHandler implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Toast.makeText(HomeActivity.this, "ye", Toast.LENGTH_SHORT);

            // Get questions
            new APITool().FetchQuestions(HomeActivity.this);
        }
    }


    private void perm()
    {
        // Here, thisActivity is the current activity
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.INTERNET) != PackageManager.PERMISSION_GRANTED)
        {

            // Should we show an explanation?
            if (ActivityCompat.shouldShowRequestPermissionRationale(this, Manifest.permission.INTERNET))
            {

                // Show an explanation to the user *asynchronously* -- don't block
                // this thread waiting for the user's response! After the user
                // sees the explanation, try again to request the permission.

            } else
            {

                // No explanation needed, we can request the permission.

                ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.INTERNET}, 0);

                // MY_PERMISSIONS_REQUEST_READ_CONTACTS is an
                // app-defined int constant. The callback method gets the
                // result of the request.
            }
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults)
    {
        switch (requestCode)
        {
            case 0:
            {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED)
                {
                    // permission was granted, yay! Do the
                    // contacts-related task you need to do.

                } else
                {
                    // permission denied, boo! Disable the
                    // functionality that depends on this permission.
                }
                return;
            }

            // other 'case' lines to check for other
            // permissions this app might request
        }
    }


    public void returnQuestions(String jsonArray)
    {
      //  questionArray = q;
      // Toast.makeText(HomeActivity.this, "ye", Toast.LENGTH_SHORT).show();

        if(jsonArray != null)
        {
            // If API call successful
            Intent intent = new Intent(this, QuestionActivity.class);
            intent.putExtra("json", jsonArray);
            startActivity(intent);
        }
        else
        {
            errorDialog();
        }

    }

    void errorDialog()
    {

        AlertDialog.Builder builder =  new AlertDialog.Builder(HomeActivity.this, android.R.style.Theme_Material_Dialog_Alert);

        builder.setMessage("An error has occurred. Check your network connection and try again.")
                .setTitle("Error")
                .setPositiveButton("OK", new DialogInterface.OnClickListener()
                {
                    public void onClick(DialogInterface dialog, int id)
                    {
                        // User clicked OK button
                        //finish();
                    }
                });


        AlertDialog dialog = builder.create();
        dialog.show();
    }
}
