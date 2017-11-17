import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  pure:false,
})
export class FilterPipe implements PipeTransform {

  transform(value: any, filteredReviews: string, propName:string,propName2:string,propName3:string): any {
    // if(value.length===0 || filteredReviews===''){
    //   return value;
    // }
    // const resultArray=[];
    // for(const item of value){
    //   if(item[propName].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0 ||
    //       item[propName2].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0 ||
    //     item[propName3].toLowerCase().indexOf(filteredReviews.toLowerCase())>=0){
    //     resultArray.push(item);
    //   }
    // }
    // return resultArray;
  }

}
