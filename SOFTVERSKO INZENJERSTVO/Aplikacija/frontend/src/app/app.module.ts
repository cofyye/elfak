import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { FeaturesModule } from './features/features.module';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpRequestInterceptor } from './shared/interceptors/http-request.interceptor';
import { ErrorHandlingInterceptor } from './shared/interceptors/error-handling.interceptor';
import { StoreModule } from '@ngrx/store';
import { appReducer } from './app.state';
import { HomeModule } from './home/home.module';
import { SocketIoModule, SocketIoConfig } from 'ngx-socket-io';
import { environment } from '../environments/environment';

const config: SocketIoConfig = {
  url: environment.SOCKET_URL,
  options: {
    withCredentials: true,
  },
};

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FeaturesModule,
    HomeModule,
    RouterModule,
    BrowserAnimationsModule,
    ToastModule,
    ProgressSpinnerModule,
    StoreModule.forRoot(appReducer),
    SocketIoModule.forRoot(config),
  ],
  providers: [
    MessageService,
    {
      provide: APP_INITIALIZER,
      useFactory: (primeConfig: PrimeNGConfig) => () => {
        primeConfig.ripple = true;
      },
      deps: [PrimeNGConfig],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpRequestInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlingInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
