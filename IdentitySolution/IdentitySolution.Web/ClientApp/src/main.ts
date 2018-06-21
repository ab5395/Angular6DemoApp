import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  //var data = environment.production;
  //alert(data);
  enableProdMode();
  //document.write('<script type="text/javascript">' + data + '</script>');
  //console.log("Production:" + data);
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));
