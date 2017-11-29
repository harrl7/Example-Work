package a.quiz;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;


/**
 * Created by Liam on 22-Nov-17.
 */

public class APITool
{

    // API base url
    private final String BASE_URL = "https://opentdb.com/api.php?amount=10&category=9&type=multiple";

    HomeActivity activity;


    public void FetchQuestions(HomeActivity activity)
    {
        this.activity = activity;
        new HttpWorkerGetQuestions().execute();
    }


    // Get List of points in collection, return to Collectio Details fragment
    public class HttpWorkerGetQuestions extends AsyncTask<String, Void, String>
    {

        @Override
        protected String doInBackground(String... params) { return HTTPGetJSON(); }

        @Override
        protected void onPostExecute(String data)
        {
            Log.d("json", data);
           //questionArray = extractJSONQuestionArray(data);

            activity.returnQuestions(extractJSONQuestionArray(data));
        }
    }


    // Process JSON from a GardenItem
    private String extractJSONQuestionArray(String data)
    {
        try
        {

            JSONObject jsonObject = new JSONObject(data);
            JSONArray questionJSONArray = jsonObject.getJSONArray("results");

            return questionJSONArray.toString();

        } catch (JSONException e) { e.printStackTrace(); }
        catch (IndexOutOfBoundsException e) { e.printStackTrace(); }

        return null;

    }


    // Fetch JSON string from web
    private String HTTPGetJSON()
    {
        String JSONString = "";

        try
        {

            // Create API URL
            String api = BASE_URL; // + apiCommand;

            String replace = api.replaceAll(" ","%20");     // Remove whitespace in query parameters
            URL url = new URL(replace);

            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");
            connection.connect();

            int code = connection.getResponseCode();
            if (code == 200)
            {
                InputStream stream = connection.getInputStream();
                InputStreamReader reader = new InputStreamReader(stream);
                BufferedReader br = new BufferedReader(reader);

                String responseString;
                StringBuilder builder = new StringBuilder();

                while ((responseString = br.readLine()) != null)
                {
                    builder = builder.append(responseString);
                }
                JSONString = builder.toString();
            }

        } catch (Exception e) { e.printStackTrace(); }

        return JSONString;
    }

}
